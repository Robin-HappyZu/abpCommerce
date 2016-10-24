using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 旅游
    /// </summary>
    /// <returns></returns>
    public class ToursController : MobileControllerBase
    {
        // GET: Mobile/Tours
        public ActionResult Index()
        {
            ViewBag.Title = "境外游";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "historyback",
                        Icon = "icon icon-left",
                        Url = "javascript:window.history.back();"
                    }
                }
            };
            return View();
        }
    }
}