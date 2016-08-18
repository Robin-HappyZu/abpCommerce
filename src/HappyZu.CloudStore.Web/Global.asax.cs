using System;
using Abp.Web;
using Castle.Facilities.Logging;
using Happyzu.CloudStore.Common.Theming;

namespace HappyZu.CloudStore.Web
{
    public class MvcApplication : AbpWebApplication<CloudStoreWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));

            ThemeManager.Instance.Configure(config =>
            {
                config.ThemeDirectory = "~/Themes";
                config.DefaultTheme = "Default";
                config.ThemeResolver = new CookiesThemeResover();
            });

            base.Application_Start(sender, e);
        }
    }
}
