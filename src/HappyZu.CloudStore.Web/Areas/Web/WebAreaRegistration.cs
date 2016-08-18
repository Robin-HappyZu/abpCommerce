using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Happyzu.CloudStore.Common.Areas;
using Happyzu.CloudStore.Common.Routing.Domain;

namespace HappyZu.CloudStore.Web.Areas.Web
{
    public class WebAreaRegistration : AreaRegistrationBase 
    {
        public override string AreaName => "Web";

        public override int Order => 999;

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "Web_default",
        //        "Web/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            var domain = ConfigurationManager.AppSettings["Domain"] ?? "happizu.com";

            var values = new Dictionary<string, object>();
            var ns = new[] { "Happyzu.CloudStore.Web.Areas.Web.Controllers" };
            values.Add("UseNamespaceFallback", false);
            values.Add("Namespaces", ns);
            values.Add("area", "web");
            var dataTokens = new RouteValueDictionary(values);


            var route = new DomainRoute(
                string.Concat("www.", domain),
                "{controller}/{action}/{id}",
                new { area = "web", controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                dataTokens
                );

            context.Routes.Add("WebDomainRoute", route);
        }
    }
}