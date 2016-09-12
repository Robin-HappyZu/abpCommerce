using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class LayoutController : Controller
    {
        [ChildActionOnly]
        public ActionResult FootMenu(string activeMenu = "")
        {
            if (string.IsNullOrWhiteSpace(activeMenu))
            {
                activeMenu = "Home";
            }

            var footMenuVM = new FootMenuViewModel()
            {
                ActiveMenuItemName = activeMenu,
                ShoppingCartItemCount = 10
            };
            return PartialView("FootMenu", footMenuVM);
        }

        [ChildActionOnly]
        public ActionResult HeaderBar(HeaderViewModel vm)
        {
            return PartialView("HeaderBar", vm);
        }
    }
}