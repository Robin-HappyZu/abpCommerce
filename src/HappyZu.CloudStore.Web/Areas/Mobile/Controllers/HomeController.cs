using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class HomeController : Controller
    {
        // GET: Mobile/Home
        public ActionResult Index()
        {
            ViewBag.Title = "首页";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = false,
                Title = ViewBag.Title,
                ShowSearchBar = true,
                LeftButtonItems = new []
                {
                    new BarButtonItem()
                    {
                        Name = "Location",
                        DisplayName = "长沙",
                        Icon = "icon icon-location",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Account",
                        //DisplayName = "个人中心",
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };
            //隐藏底部
            ViewBag.HideFootBar = true;
            return View();
        }
    }
}