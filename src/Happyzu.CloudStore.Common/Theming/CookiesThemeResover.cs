using System.Web;
using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Theming
{
    public class CookiesThemeResover : IThemeResolver
    {
        public string Resolve(ControllerContext controllerContext, string theme)
        {
            var result = string.Empty;

            if (controllerContext.HttpContext.Request.Cookies != null && controllerContext.HttpContext.Request.Cookies.Get("Theme") != null)
            {
                var httpCookie = controllerContext.HttpContext.Request.Cookies.Get("Theme");
                if (httpCookie != null)
                    result = httpCookie.Value;
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = "Default";
                }
            }
            else
            {
                result = (!string.IsNullOrEmpty(theme) ? theme : "Default");
            }

            return result;
        }

        public void SetTheme(ControllerContext controllerContext, string theme)
        {
            if (controllerContext.HttpContext.Request.Cookies != null)
            {
                var httpCookies=new HttpCookie("Theme",theme);
                controllerContext.HttpContext.Request.Cookies.Set(httpCookies);
            }
        }
    }
}
