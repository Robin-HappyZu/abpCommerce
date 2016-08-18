using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Happyzu.CloudStore.Common.Theming.Attributes
{
    public class LayoutAttribute: ActionFilterAttribute
    {
        public LayoutAttribute(string layout)
        {
            Layout = layout;
        }
        public string Layout { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RouteData.Values.Add("Layout", Layout);
            base.OnActionExecuting(filterContext);
        }
    }
}
