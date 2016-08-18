using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Theming
{
    public interface IThemeResolver
    {
        string Resolve(ControllerContext controllerContext, string theme);

        void SetTheme(ControllerContext controllerContext, string theme);
    }
}
