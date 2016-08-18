using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Theming
{
    public class SessionThemeResover : IThemeResolver
    {
        public string Resolve(ControllerContext controllerContext, string theme)
        {
            string result;

            if (controllerContext.HttpContext.Session != null && controllerContext.HttpContext.Session["Theme"] != null)
            {
                result = controllerContext.HttpContext.Session["Theme"].ToString();
            }
            else
            {
                result = (!string.IsNullOrEmpty(theme) ? theme : "Default");
            }

            return result;
        }

        public void SetTheme(ControllerContext controllerContext, string theme)
        {
            if (controllerContext.HttpContext.Session != null)
            {
                controllerContext.HttpContext.Session["Theme"] = theme;
            }
        }
    }
}
