using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Theming
{
    public class DefaultThemeResolver : IThemeResolver
    {
        public string Resolve(ControllerContext controllerContext, string theme)
        {
            string themeRouteParam = controllerContext.RouteData.Values.ContainsKey("Theme") ? controllerContext.RouteData.Values["Theme"].ToString() : null;

            return themeRouteParam ?? (!string.IsNullOrEmpty(theme) ? theme : "Default");
        }

        public void SetTheme(ControllerContext controllerContext, string theme)
        {

        }
    }
}
