using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Dest;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using HappyZu.CloudStore.Web.Controllers;
using Abp.Extensions;
using Abp.UI;
using HappyZu.CloudStore.Web.Areas.Mobile.Models;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 门票
    /// </summary>
    public class TicketController : MobileControllerBase
    {
        private readonly IDestAppService _destAppService;
        private readonly ITicketAppService _ticketAppService;
        public TicketController(IDestAppService destAppService, ITicketAppService ticketAppService)
        {
            _destAppService = destAppService;
            _ticketAppService = ticketAppService;
        }

        // GET: Mobile/Ticket
        #region 景点列表
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
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };

            return View();
        }

        #endregion

        #region 景点详情

        public async Task<ViewResult> Detail(int id)
        {

            var dest = await _destAppService.GetDestByIdAsync(id);
            var tickets = await _ticketAppService.GetPagedTicketsAsync(new Trip.Dto.GetPagedTicketsInput
            {
                DestId = dest.Id
            });
            var ticketType = await _ticketAppService.GetTicketTypeListAsync(dest.Id);
            var pictures = await _destAppService.GetPagedDestPicturesAsync(new FileManager.Dto.GetPagedFileItemInput
            {
                MappingId=dest.Id,
                MaxResultCount=10
            });
            var city = await _destAppService.GetDestCityByIdAsync(dest.CityId);
            var province = await _destAppService.GetDestProvinceByIdAsync(dest.ProvinceId);

            var vm = new DetailViewModel()
            {
                Dest = dest,
                Tickets = tickets,
                TicketTypes=ticketType,
                Pictures=pictures,
                City=city,
                Province=province
            };

            ViewBag.Title = dest.Title;

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
                        Icon = "icon iconfont icon-like",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };

            return View(vm);
        }

        public async Task<PartialViewResult> DestAbout(int id)
        {
            var dest = await _destAppService.GetDestByIdAsync(id);
            return PartialView(dest);
        }
        #endregion

        #region 门票列表

        public async Task<JsonResult> GetTicketsByDest(int id)
        {
            var tickets = await _ticketAppService.GetPagedTicketsAsync(new GetPagedTicketsInput()
            {
                DestId = id
            });
            var ticketType = await _ticketAppService.GetTicketTypeListAsync(id);
            return Json(new{tickets= tickets,ticketType=ticketType});
        }
        #endregion

        #region 门票报价

        public async Task<JsonResult> GetQuotesByTicket(GetQuotesViewModel vm )
        {
            var quotes = await _ticketAppService.GetPagedTicketQuotesByTicektId(new GetPagedTicketQuotesInput
            {
                TicketId = vm.Id,
                MaxDate=vm.MaxDate,
                MinDate=vm.MinDate,
                IsDisplay=true
            });
            return Json(quotes);
        }
        #endregion

        #region 订单填写

        public async Task<ViewResult> TicketOrder(int id,int ticket)
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
                        Url = Url.Action("Detail","Ticket", new {area="Mobile",id=id},true)
                    }
                }
            };

            var ticketEntity = await _ticketAppService.GetTicketByIdAsync(ticket);
            var ticketQuotes = await _ticketAppService.GetPagedTicketQuotesByTicektId(new GetPagedTicketQuotesInput()
            {
                TicketId = ticket,
                MaxResultCount = 3,
                MinDate = DateTime.Today,
                IsDisplay = true
            });
            var vm = new TicketOrderViewModel
            {
                Ticket = ticketEntity,
                TicketQuotes = ticketQuotes.Items
            };
            return View(vm);
        }
        #endregion

        #region 生成订单

        public async Task<JsonResult> ConfirmTicketOrder(ConfirmTicketOrderViewModel vm)
        {
            var agent = Request.Cookies.Get("agent");

            if (string.IsNullOrWhiteSpace(vm.Mobile)||string.IsNullOrWhiteSpace(vm.Contact))
            {
                throw new UserFriendlyException("取票人和联系电话不能为空", "取票人或联系电话信息错误");
            }

            if (vm.Tickets==null || vm.Tickets.Count<1)
            {
                throw new UserFriendlyException("必须选择1种门票", "门票信息错误");
            }

            var input = new AddTicketOrderInput()
            {
                Tickets=vm.Tickets,
                Mobile=vm.Mobile,
                Contact=vm.Contact,
                Remark=vm.Remark
            };
            if (agent != null)
            {
                var agentid = 0;
                if (int.TryParse(agent.Value, out agentid))
                {
                    input.AgentId = agentid;
                }
            }
            var result = await _ticketAppService.AddTicketOrderAsync(input);
            return Json(result);

        }
        #endregion

        #region 生成订单失败

        #endregion

        #region 选择支付方式

        public async Task<ViewResult> ChoosePayment(int id)
        {
            ViewBag.Title = "支付方式";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "TicketOrder",
                        //DisplayName = "长沙",
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile"},true)
                    }
                }
            };
            var order = await _ticketAppService.GetTicketOrderByIdAsync(id);

            return View(order);
        }
        #endregion

        #region 订单支付

        public ActionResult PaymentOrder()
        {
            return View();
        }
        #endregion

        #region 支付成功

        public async Task<ViewResult> PaymentSuccess(int id)
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

            var order = await _ticketAppService.GetTicketOrderByIdAsync(id);

            return View(order);
        }
        #endregion

        #region 支付失败

        public async Task<ViewResult> PaymentFail(int id)
        {
            ViewBag.Title = "支付失败";
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
            var order = await _ticketAppService.GetTicketOrderByIdAsync(id);

            return View(order);
        }
        #endregion
    }
}