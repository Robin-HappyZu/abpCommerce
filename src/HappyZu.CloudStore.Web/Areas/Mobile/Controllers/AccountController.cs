using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using HappyZu.CloudStore.Web.Controllers;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class AccountController : MobileControllerBase
    {
        // GET: Mobile/Account
        public ActionResult Index()
        {
            ViewBag.Title = "个人中心";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                     new BarButtonItem()
                    {
                        Name = "Home",
                        Icon = "icon icon-home",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                     new BarButtonItem()
                    {
                        Name = "Settings",
                        Icon = "icon icon-settings",
                        Url = Url.Action("Settings","Account", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }
        public ActionResult Settings()
        {
            ViewBag.Title = "设置";
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