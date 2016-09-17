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
    /// <summary>
    /// 门票
    /// </summary>
    public class TicketController : MobileControllerBase
    {
        // GET: Mobile/Ticket
        #region 门票列表
        public ActionResult Index()
        {
            ViewBag.Title = "长沙";
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
                        //DisplayName = "长沙",
                        Icon = "icon icon-left",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Search",
                        //DisplayName = "个人中心",
                        Icon = "icon icon-search",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };

            return View();
        }

        #endregion

        #region 门票详情

        public ActionResult Detail()
        {
            ViewBag.Title = "飞行家室内飞行体验馆";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Tickets",
                        //DisplayName = "长沙",
                        Icon = "icon icon-left",
                        Url = Url.Action("Index","Ticket", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Favorites",
                        //DisplayName = "个人中心",
                        Icon = "icon icon-star",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };
            return View();
        }
        #endregion
    }
}