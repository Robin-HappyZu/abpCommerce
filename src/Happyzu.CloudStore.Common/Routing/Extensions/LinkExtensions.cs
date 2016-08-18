using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Happyzu.CloudStore.Common.Routing.Domain;
using Happyzu.CloudStore.Common.Routing;

namespace System.Web.Mvc.Html
{
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), htmlAttributes, requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, htmlAttributes, requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), htmlAttributes, requireAbsoluteUrl);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
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
                    //routeValues.Values = routeValues.Values;
                    routeData.Values["area"] = area;
                    routeData.Values["controller"] = controllerName;
                    routeData.Values["action"] = actionName;

                    DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
                    return htmlHelper.ActionLink(linkText, actionName, controllerName, domainData.Protocol, domainData.HostName, domainData.Fragment, routeData.Values, htmlAttributes);
                    
                }
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }
    }
}
