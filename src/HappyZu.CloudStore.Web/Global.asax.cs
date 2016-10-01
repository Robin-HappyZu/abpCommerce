using System;
using Abp.Web;
using Castle.Facilities.Logging;
using Happyzu.CloudStore.Common.Theming;
using Senparc.Weixin.MP.TenPayLibV3;

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

            RegisterWeixinPay();

            base.Application_Start(sender, e);
        }

        /// <summary>
        /// 注册微信支付
        /// </summary>
        private void RegisterWeixinPay()
        {
            //提供微信支付信息
            var tenPayV3_MchId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"];
            var tenPayV3_Key = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_Key"];
            var tenPayV3_AppId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppId"];
            var tenPayV3_AppSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppSecret"];
            var tenPayV3_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_TenpayNotify"];

            var tenPayV3Info = new TenPayV3Info(tenPayV3_AppId, tenPayV3_AppSecret, tenPayV3_MchId, tenPayV3_Key,
                                                tenPayV3_TenpayNotify);
            TenPayV3InfoCollection.Register(tenPayV3Info);
        }
    }
}
