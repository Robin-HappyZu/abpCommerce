using System.Web.Mvc;
using Happyzu.CloudStore.Common.Theming.Configuration;

namespace Happyzu.CloudStore.Common.Theming.ViewEngine
{
    public class ThemeableRazorViewEngine : RazorViewEngine
    {
        private readonly IThemingConfiguration _configuration;

        public ThemeableRazorViewEngine(IThemingConfiguration configuration)
        {
            _configuration = configuration;

            AreaViewLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/{2}/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };
            AreaMasterLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/{2}/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };
            AreaPartialViewLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/{2}/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            ViewLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/Views/Shared/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/Views/Shared/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            MasterLocationFormats = new[]
            {
                "$ThemeDirectory/$Theme/Views/{1}/{0}.cshtml",
                "$ThemeDirectory/$Theme/Views/Shared/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (!string.IsNullOrWhiteSpace(masterName))
                return base.FindView(controllerContext, viewName, masterName, useCache);

            if (controllerContext.RouteData.Values.ContainsKey("Layout"))
            {
                var layout = controllerContext.RouteData.Values["Layout"].ToString();

                if (string.IsNullOrWhiteSpace(layout))
                    return base.FindView(controllerContext, viewName, masterName, useCache);

                layout = layout.Replace("-", "_");
                if (!layout.StartsWith("_"))
                {
                    layout = string.Concat("_", layout);
                }
                masterName = layout;
            }
            else
            {
                masterName = _configuration.DefaultLayoutName;
            }

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            partialPath = GetThemedPath(controllerContext, partialPath);
            return base.CreatePartialView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            masterPath = GetThemedPath(controllerContext, masterPath);
            viewPath = GetThemedPath(controllerContext, viewPath);
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            virtualPath = GetThemedPath(controllerContext, virtualPath);
            return base.FileExists(controllerContext, virtualPath);
        }

        private string GetThemedPath(ControllerContext controllerContext, string virtualPath)
        {
            if (string.IsNullOrWhiteSpace(virtualPath))
            {
                return virtualPath;
            }

            var themeDirectory = _configuration.ThemeDirectory;
            var theme = _configuration.ThemeResolver.Resolve(controllerContext, _configuration.DefaultTheme);
            virtualPath = virtualPath.Replace("$ThemeDirectory", themeDirectory).Replace("$Theme", theme);

            return virtualPath;
        }
    }
}
