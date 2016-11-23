using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Filters;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.WechatPay;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;
using Abp.Extensions;
using HappyZu.CloudStore.Wechat.Dto;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class WechatPayController : MobileControllerBase
    {
        private readonly IPaymentAppService _paymentAppService;
        private static TenPayV3Info _tenPayV3Info;

        public static TenPayV3Info TenPayV3Info
        {
            get {
                return _tenPayV3Info ??
                       (_tenPayV3Info =
                           TenPayV3InfoCollection.Data[
                               ConfigurationManager.AppSettings["TenPayV3_MchId"]]);
            }
        }

        public WechatPayController(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }

        [WechatAuthFilter]
        // GET: Mobile/WechatPay
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 微信统一下单，返回支付参数
        /// </summary>
        /// <returns></returns>
        public ActionResult JsApiUnifiedorder()
        {
            // 订单号
            // 订单价格
            string openId = "";
            string orderId = "";
            string orderTotalFee = "";
            string orderDescription = "";

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", TenPayV3Info.AppId);		    //公众账号ID
            packageReqHandler.SetParameter("mch_id", TenPayV3Info.MchId);		    //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);                  //随机字符串
            packageReqHandler.SetParameter("body", orderDescription);               //商品信息
            packageReqHandler.SetParameter("out_trade_no", orderId);		        //商家订单号
            packageReqHandler.SetParameter("total_fee", orderTotalFee);			    //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);        //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", TenPayV3Info.TenPayV3Notify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());	    //交易类型
            packageReqHandler.SetParameter("openid", openId);	                    //用户的openId

            string sign = packageReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);
            packageReqHandler.SetParameter("sign", sign);	                        //签名

            string data = packageReqHandler.ParseXML();


            try
            {
                var result = TenPayV3.Unifiedorder(data);
                var res = XDocument.Parse(result);
                string prepayId = res.Element("xml").Element("prepay_id").Value;

                //设置支付参数
                RequestHandler paySignReqHandler = new RequestHandler(null);
                paySignReqHandler.SetParameter("appId", TenPayV3Info.AppId);
                paySignReqHandler.SetParameter("timeStamp", timeStamp);
                paySignReqHandler.SetParameter("nonceStr", nonceStr);
                paySignReqHandler.SetParameter("package", $"prepay_id={prepayId}");
                paySignReqHandler.SetParameter("signType", "MD5");
                var paySign = paySignReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);

                var model = new WechatPayParamModel()
                {
                    appId = TenPayV3Info.AppId,
                    timeStamp = timeStamp,
                    nonceStr = nonceStr,
                    packageValue = $"prepay_id={prepayId}",
                    paySign = paySign,
                    msg = "下单成功"
                };

                return Json(model);
            }
            catch (Exception)
            {
                var model = new WechatPayParamModel()
                {
                    msg = "下单成功失败,请重试"
                };

                return Json(model);
            }
        }

        /// <summary>
        /// 微信支付回调地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Notify()
        {
            var responseHandler = new ResponseHandler(null);

            var return_code = responseHandler.GetParameter("return_code");
            var return_msg = responseHandler.GetParameter("return_msg");
            var tradeNo = responseHandler.GetParameter("out_trade_no");

            string res = null;
            responseHandler.SetKey(TenPayV3Info.Key);
            // 验证请求是否微信发过来
            if (responseHandler.IsTenpaySign())
            {
                res = "success";
                var orderNo = tradeNo.Split('|')[1];
                var input = new OrderPaidInput();
                var wechatResult = new WechatPayResult()
                {
                    OrderNo= orderNo,
                    IsSubscribe= responseHandler.GetParameter("is_subscribe")=="Y",
                    OpenId=responseHandler.GetParameter("openid"),
                    TransactionNo=responseHandler.GetParameter("transaction_id")
                };

                int amount;
                int.TryParse(responseHandler.GetParameter("total_fee"),out amount);
                int cashAmount;
                int.TryParse(responseHandler.GetParameter("cash_fee"), out cashAmount);

                DateTime paidTime;
                DateTime.TryParseExact(responseHandler.GetParameter("time_end"), "yyyyMMddHHmmss",null, System.Globalization.DateTimeStyles.None ,out paidTime);
                wechatResult.Amount = (decimal)(amount/100.00);
                wechatResult.CashAmount = (decimal)(cashAmount / 100.00);
                wechatResult.PaidTime = paidTime;

                input.WechatPayResult = wechatResult;
                // 发布付款成功事件
                _paymentAppService.OrderPaidAsync(input);
            }
            else
            {
                res = "wrong";
            }

            string xml = string.Format(@"<xml>
   <return_code><![CDATA[{0}]]></return_code>
   <return_msg><![CDATA[{1}]]></return_msg>
</xml>", return_code, return_msg);

            return Content(xml, "text/xml");
        }
        

        public ActionResult TestNotity()
        {
            var tradeNo = "ticket|76494407737344|150015";
            var orderNo = tradeNo.Split('|')[1];
            var input = new OrderPaidInput();
            var wechatResult = new WechatPayResult()
            {
                OrderNo = orderNo,
                IsSubscribe = true,
                OpenId = "oJBwJwXdiqvWzkQ3wverskTTOLvY",
                TransactionNo = "4010132001201611229813214581"
            };

            int amount;
            int.TryParse("10000", out amount);
            int cashAmount;
            int.TryParse("10000", out cashAmount);

            DateTime paidTime;
            DateTime.TryParseExact("20161123151721", "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out paidTime);
            wechatResult.Amount = (decimal)(amount / 100.00);
            wechatResult.CashAmount = (decimal)(cashAmount / 100.00);
            wechatResult.PaidTime = paidTime;

            input.WechatPayResult = wechatResult;
            // 发布付款成功事件
            _paymentAppService.OrderPaidAsync(input);

            string xml = string.Format(@"<xml>
   <return_code><![CDATA[{0}]]></return_code>
   <return_msg><![CDATA[{1}]]></return_msg>
</xml>", "SUCCESS", "支付成功");

            return Content(xml, "text/xml");
        }
    }
}