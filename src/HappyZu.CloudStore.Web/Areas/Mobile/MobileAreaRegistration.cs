using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Happyzu.CloudStore.Common.Areas;
using Happyzu.CloudStore.Common.Routing.Domain;

namespace HappyZu.CloudStore.Web.Areas.Mobile
{
    public class MobileAreaRegistration : AreaRegistrationBase 
    {
        public override string AreaName => "Mobile";

        public override int Order => 1000;

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "Mobile_default",
        //        "Mobile/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            // context.MapRoute(
            //    "Mobile_default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "HappyZu.CloudStore.Web.Areas.Mobile.Controllers" }
            //);

            var domain = ConfigurationManager.AppSettings["Domain"] ?? "happizu.com";

            var values = new Dictionary<string, object>();
            var ns = new[] { "Happyzu.CloudStore.Web.Areas.Mobile.Controllers" };
            values.Add("UseNamespaceFallback", false);
            values.Add("Namespaces", ns);
            values.Add("area", "mobile");
            var dataTokens = new RouteValueDictionary(values);


            var route = new DomainRoute(
                string.Concat("m.", domain),
                "{controller}/{action}/{id}",
                new { area = "mobile", controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                dataTokens
                );

            context.Routes.Add("MobileDomainRoute", route);
        }
    }
}