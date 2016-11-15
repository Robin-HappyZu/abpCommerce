using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Xml.Linq;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.WechatPay;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class PaymentController : MobileControllerBase
    {
        private readonly ITicketAppService _ticketAppService;
        private readonly IUserAppService _userAppService;
        private static TenPayV3Info _tenPayV3Info;

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }


        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"]];
                }
                return _tenPayV3Info;
            }
        }

        public PaymentController(ITicketAppService ticketAppService, IUserAppService userAppService)
        {
            _ticketAppService = ticketAppService;
            _userAppService = userAppService;
        }

        // GET: Mobile/Payment
        public async Task<ActionResult> Ticket(int id)
        {
            ViewBag.Title = "支付方式";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "TicketOrder",
                        //DisplayName = "长沙",
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile"},true)
                    }
                }
            };
            ViewBag.AppId = TenPayV3Info.AppId;
            var order = await _ticketAppService.GetTicketOrderByIdAsync(id);
            return View(order);
        }

       
        [AbpMvcAuthorize]
        public async Task<JsonResult> JsApi(int orderId)
        {
            var userId = AbpSession.GetUserId();
            var userDto =await _userAppService.GetUserByIdAsync(userId);
            if (string.IsNullOrWhiteSpace(userDto.WechatOpenID))
            {
                //userDto.WechatOpenID = "otimDwgMGfA75NxvZ54O4RU0wRXU";
                return Json(new WechatPayParamModel
                {
                    msg = "OpenIdIsEmpty"
                });
            }

            var orderDto =await _ticketAppService.GetTicketOrderByIdAsync(orderId);

            if (orderDto == null)
            {
                return Json(null);
            }

            var timeStamp ="";
            var nonceStr = "";
            var paySign = "";
            
            //当前时间 yyyyMMdd
            var date = DateTime.Now.ToString("yyyyMMdd");

            var sp_billno = string.Concat("ticket|",orderDto.OrderNo, "|", DateTime.Now.ToString("HHmmss")); //+ TenPayV3Util.BuildRandomStr(28);

            //创建支付应答对象
            var packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

            timeStamp = TenPayV3Util.GetTimestamp();
            nonceStr = TenPayV3Util.GetNoncestr();
            var notifyUrl = Url.Action("Notify", "WechatPay", new {area = "Mobile"}, true);
            //设置package订单参数
            packageReqHandler.SetParameter("appid", TenPayV3Info.AppId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", TenPayV3Info.MchId);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);                    //随机字符串
            packageReqHandler.SetParameter("body", "环球舱-旅游门票");    //商品信息
            packageReqHandler.SetParameter("out_trade_no", sp_billno);      //商家订单号
#if DEBUG
            packageReqHandler.SetParameter("total_fee", "1");
#else
            packageReqHandler.SetParameter("total_fee", (orderDto.TotalAmount*100).ToString());	        
            //商品金额,以分为单位(money * 100).ToString()
#endif
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);        //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", notifyUrl);		    //TenPayV3Info.TenPayV3Notify  接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());	    //交易类型
            packageReqHandler.SetParameter("openid", userDto.WechatOpenID);	                    //用户的openId

            var sign = packageReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);
            packageReqHandler.SetParameter("sign", sign);	                    //签名

            var data = packageReqHandler.ParseXML();

            var result = TenPayV3.Unifiedorder(data);
            var res = XDocument.Parse(result);
            var xElement = res.Element("xml");
            if (xElement == null) return Json(null);

            var element = xElement.Element("prepay_id");
            if (element == null)
            {
                return Json(new WechatPayParamModel
                {
                    msg= xElement.Element("err_code")?.Value
                });
            }

            var prepayId = element.Value;

            //设置支付参数
            var paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appId", TenPayV3Info.AppId);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("package", $"prepay_id={prepayId}");
            paySignReqHandler.SetParameter("signType", "MD5");
            paySign = paySignReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);
            
            var vm = new WechatPayParamModel()
            {
                appId = TenPayV3Info.AppId,
                timeStamp= timeStamp,
                nonceStr= nonceStr,
                packageValue= $"prepay_id={prepayId}",
                paySign= paySign
            };
            return Json(vm);
        }

        public ActionResult Native()
        {
            return View();
        }

        public ActionResult NativeCall()
        {
            return View();
        }


    }
}