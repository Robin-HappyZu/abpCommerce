using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class TicketOrderController : AdminControllerBase
    {
        private readonly ITicketAppService _ticketAppService;

        public TicketOrderController(ITicketAppService ticketAppService)
        {
            _ticketAppService = ticketAppService;
        }

        // GET: Admin/TicketOrder
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetTicketOrders(GetTicketOrdersViewModel model)
        {
            var input = new GetPagedTicketOrdersInput()
            {
                MaxResultCount = model.length,
                SkipCount = model.start
            };

            var output = await _ticketAppService.GetTicketOrdersAsync(input);

            var vm = new DataTableJsonViewModel()
            {
                draw = model.draw,
                customActionMessage = "",
                customActionStatus = "OK"
            };

            try
            {
                vm.recordsFiltered = vm.recordsTotal = output.TotalCount;
                vm.data = output.Items.Select(x => new
                {
                    x.Id,
                    x.OrderNo,
                    x.Status,
                    x.Contact,
                    x.Mobile,
                    x.Email,
                    x.IsVerification,
                    x.CreationTime
                }).ToList();
            }
            catch (Exception ex)
            {
                vm.customActionMessage = ex.Message;
                vm.customActionStatus = "";
            }

            if (vm.data == null) vm.data = new List<object>();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderDetail(int orderId)
        {
            return View();
        }

        public JsonResult GetTicketOrderItems()
        {
            var vm = new DataTableJsonViewModel();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}