namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.WechatPay
{
    public class WechatPayParamModel
    {
        public string appId { get; set; }
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string packageValue { get; set; }
        public string paySign { get; set; }
        public string msg { get; set; }
    }
}