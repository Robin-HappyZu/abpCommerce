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
                        Name = "ShoppingCart",
                        //DisplayName = "购物车",
                        Icon = "icon icon-cart",
                        Url = Url.Action("Index","ShoppingCart", new {area="Mobile",type="empty"},true)
                    }
                }
            };
            //隐藏底部
            //ViewBag.HideFootBar = true;
            return View();
        }
    }
}