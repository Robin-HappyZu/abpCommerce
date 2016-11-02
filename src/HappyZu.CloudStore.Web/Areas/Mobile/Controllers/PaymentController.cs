using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Xml.Linq;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class PaymentController : MobileControllerBase
    {
        private readonly ITicketAppService _ticketAppService;
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

        public PaymentController(ITicketAppService ticketAppService)
        {
            _ticketAppService = ticketAppService;
        }

        // GET: Mobile/Payment
        public async Task<ActionResult> Index(int id)
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
            var order = await _ticketAppService.GetTicketOrderByIdAsync(id);
            return View(order);
        }

       

        public ActionResult JsApi(string openId)
        {

            string timeStamp = "";
            string nonceStr = "";
            string paySign = "";

            string sp_billno = Request["order_no"];
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenPayV3Util.BuildRandomStr(28);
            }
            else
            {
                sp_billno = Request["order_no"].ToString();
            }

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

            timeStamp = TenPayV3Util.GetTimestamp();
            nonceStr = TenPayV3Util.GetNoncestr();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", TenPayV3Info.AppId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", TenPayV3Info.MchId);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);                    //随机字符串
            packageReqHandler.SetParameter("body", "商品信息");    //商品信息
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("total_fee", "1");//product == null ? "100" : (product.Price * 100).ToString());			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", TenPayV3Info.TenPayV3Notify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());	                    //交易类型
            packageReqHandler.SetParameter("openid", openId);	                    //用户的openId

            string sign = packageReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);
            packageReqHandler.SetParameter("sign", sign);	                    //签名

            string data = packageReqHandler.ParseXML();

            var result = TenPayV3.Unifiedorder(data);
            var res = XDocument.Parse(result);
            string prepayId = res.Element("xml").Element("prepay_id").Value;

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appId", TenPayV3Info.AppId);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayId));
            paySignReqHandler.SetParameter("signType", "MD5");
            paySign = paySignReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);

            ViewData["appId"] = TenPayV3Info.AppId;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["package"] = string.Format("prepay_id={0}", prepayId);
            ViewData["paySign"] = paySign;
            return View();
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