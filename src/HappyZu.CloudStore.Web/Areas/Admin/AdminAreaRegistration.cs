using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Happyzu.CloudStore.Common.Areas;
using Happyzu.CloudStore.Common.Routing.Domain;

namespace HappyZu.CloudStore.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistrationBase
    {
        public override string AreaName => "Admin";

        public override int Order =>10;

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "Admin_default",
        //        "Admin/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            var domain = ConfigurationManager.AppSettings["Domain"] ?? "happizu.com";

            var values = new Dictionary<string, object>();
            var ns = new[] { "Happyzu.CloudStore.Web.Areas.Admin.Controllers" };
            values.Add("UseNamespaceFallback", false);
            values.Add("Namespaces", ns);
            values.Add("area", "admin");
            var dataTokens = new RouteValueDictionary(values);


            var route = new DomainRoute(
                string.Concat("admin.", domain),
                "{controller}/{action}/{id}",
                new { area = "admin", controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                null,
                dataTokens
                );

            context.Routes.Add("AdminDomainRoute", route);
        }
    }
}