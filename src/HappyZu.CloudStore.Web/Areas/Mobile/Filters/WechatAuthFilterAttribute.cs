using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class WechatAuthFilterAttribute : AuthorizeAttribute
    {
        public const string USER_OAUTH2_BASE_URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
        private string appId = ConfigurationManager.AppSettings["AppId"];

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userAgent = filterContext.RequestContext.HttpContext.Request.UserAgent;
            // 检查是否是微信客户端访问
            if (string.IsNullOrWhiteSpace(userAgent) || !userAgent.Contains("MicroMessager"))
            {
                // redirect 提示页面(请通过服务号访问）
                filterContext.Result = new JsonResult
                {
                    Data = new { IsSuccess = false, Message = "不好意思,请登录微信后访问操作!" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            base.OnAuthorization(filterContext);
        }

        // 请求未被授权
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            // redirect 
            var redirect = string.Concat("http://", filterContext.HttpContext.Request.Url.Host, "Wechat/AuthCallback");

            filterContext.Result = new RedirectResult(string.Format(USER_OAUTH2_BASE_URL, appId, redirect, 0));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // 检查时候已经检查授权过
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}