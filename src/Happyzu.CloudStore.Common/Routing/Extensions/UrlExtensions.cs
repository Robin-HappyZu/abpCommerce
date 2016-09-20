using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Happyzu.CloudStore.Common.Routing.Domain;

namespace System.Web.Mvc.Html
{
    public static class UrlExtensions
    {
        public static string Action(this UrlHelper url, string actionName, bool requireAbsoluteUrl)
        {
            return url.Action(actionName,null,null, requireAbsoluteUrl);
        }
        public static string Action(this UrlHelper url, string actionName, string controllerName, bool requireAbsoluteUrl)
        {
            return url.Action(actionName, controllerName, null, requireAbsoluteUrl);
        }
        public static string Action(this UrlHelper url, string actionName, string controllerName, object routeValues, bool requireAbsoluteUrl)
        {
            return url.Action(actionName, controllerName, new RouteValueDictionary(routeValues), requireAbsoluteUrl);
        }

        public static string Action(this UrlHelper url, string actionName, string controllerName, RouteValueDictionary routeValues, bool requireAbsoluteUrl)
        {
            if (requireAbsoluteUrl)
            {
                var area = string.Empty;
                var controller = string.Empty;

                HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);

                RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

                if (routeValues != null)
                {
                    foreach (var item in routeValues)
                    {
                        if (item.Key.ToLower() == "area")
                        {
                            area = item.Value?.ToString() ?? string.Empty;
                        }
                        if (item.Key.ToLower() == "controller")
                        {
                            controller = item.Value?.ToString() ?? string.Empty;
                        }
                        routeData.Values[item.Key] = item.Value;
                    }
                    if (!routeValues.ContainsKey("Id")&& routeData.Values.ContainsKey("Id"))
                    {
                        routeData.Values.Remove("Id");
                    }
                }

                if (string.IsNullOrWhiteSpace(controllerName))
                {
                    controllerName = controller;
                }


                var domainRoutes = RouteTable.Routes.Where(x => x.GetType() == typeof(DomainRoute));
                DomainRoute domainRoute = null;

                if (!string.IsNullOrWhiteSpace(area))
                {
                    foreach (var item in domainRoutes)
                    {
                        var domain = (DomainRoute)item;
                        var domainArea = domain.Defaults["area"]?.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(domainArea) && string.Equals(domainArea, area, StringComparison.CurrentCultureIgnoreCase))
                        {
                            domainRoute = domain;
                            break;
                        }
                    }
                }
                else
                {
                    domainRoute = routeData.Route as DomainRoute;
                }

                //DomainRoute domainRoute = routeData.Route as DomainRoute;
                if (domainRoute != null)
                {
                    //routeData.DataTokens = domainRoute.DataTokens;
                    //var newRouteData = new RouteData(domainRoute, new MvcRouteHandler());
                    //newRouteData.Values["area"] = area;
                    //newRouteData.Values["controller"] = controllerName;
                    //newRouteData.Values["action"] = actionName;
                    routeData.Values["area"] = area;
                    routeData.Values["controller"] = controllerName;
                    routeData.Values["action"] = actionName;

                    DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
                    
                    return url.Action(actionName, controllerName, routeData.Values, domainData.Protocol, domainData.HostName);

                }
            }

            return url.Action(actionName, controllerName, routeValues);
        }
    }
}
