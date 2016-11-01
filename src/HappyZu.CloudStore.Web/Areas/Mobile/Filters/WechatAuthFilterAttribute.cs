using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Util;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class WechatAuthFilterAttribute : AuthorizeAttribute
    {
        private string appId = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private string domain = ConfigurationManager.AppSettings["Domain"];

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userAgent = filterContext.RequestContext.HttpContext.Request.UserAgent;
            // 检查是否是微信客户端访问
            if (string.IsNullOrWhiteSpace(userAgent) || !userAgent.Contains("MicroMessenger"))
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
            //var redirect = string.Concat("http://", filterContext.HttpContext.Request.Url.Host, "Wechat/AuthCallback");
            var redirect = string.Concat("http://", "m.", domain,"/", "Wechat/AuthCallback");
            var url = OAuthApi.GetAuthorizeUrl(appId, redirect, filterContext.HttpContext.Request.RawUrl, OAuthScope.snsapi_base);

            filterContext.Result = new RedirectResult(url);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // 检查时候已经检查授权过
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}