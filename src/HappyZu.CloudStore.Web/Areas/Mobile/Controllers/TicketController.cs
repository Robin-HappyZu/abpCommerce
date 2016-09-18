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

        #region 订单填写

        public ActionResult TicketOrder()
        {
            ViewBag.Title = "订单填写";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "TicketDetail",
                        //DisplayName = "长沙",
                        Icon = "icon icon-left",
                        Url = Url.Action("Detail","Ticket", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }
        #endregion

        #region 生成订单

        #endregion

        #region 生成订单失败

        #endregion

        #region 选择支付方式

        public ActionResult ChoosePayment()
        {
            ViewBag.Title = "支付方式";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "TicketOrder",
                        //DisplayName = "长沙",
                        Icon = "icon icon-left",
                        Url = Url.Action("TicketOrder","Ticket", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }
        #endregion

        #region 订单支付

        public ActionResult PaymentOrder()
        {
            return View();
        }
        #endregion

        #region 支付成功

        public ActionResult PaymentSuccess()
        {
            ViewBag.Title = "支付成功";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Account",
                        //DisplayName = "长沙",
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }
        #endregion

        #region 支付失败

        public ActionResult PaymentFail()
        {
            return View();
        }
        #endregion
    }
}