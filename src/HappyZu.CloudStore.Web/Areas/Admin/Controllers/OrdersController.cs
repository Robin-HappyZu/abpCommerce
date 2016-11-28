using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using HappyZu.CloudStore.Agents;
using HappyZu.CloudStore.Agents.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class OrdersController : AdminControllerBase
    {
        private readonly IRebateAppService _rebateAppService;

        public OrdersController(IRebateAppService rebateAppService)
        {
            _rebateAppService = rebateAppService;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View();
        }

        #region 代理商返利

        /// <summary>
        /// 代理商返利
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentOrders()
        {
            return View();
        }
        
        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAgentOrders(GetAgentOrdersViewModel option)
        {
            var output = await _rebateAppService.QueryListAsync(new QueryRebatesInput()
            {
                MaxResultCount = option.length,
                SkipCount = option.start
            });

            var vm = new DataTableJsonViewModel()
            {
                draw = option.draw,
                recordsTotal = 0,
                recordsFiltered = 0,
                customActionMessage = "",
                customActionStatus = "OK"
            };

            try
            {

                vm.recordsFiltered = vm.recordsTotal = output.TotalCount;

                vm.data = output.Items.Select(x => new
                {
                    AgentId = x.AgentId,
                    ExpectedRebateDate = x.ExpectedRebateDate,
                    Id = x.Id,
                    IncomeAmount = x.IncomeAmount,
                    OrderAmount = x.OrderAmount,
                    OrderNo = x.OrderNo,
                    OrderType = x.OrderType,
                    PaidTime = x.PaidTime,
                    RebateAmount = x.RebateAmount,
                    RebateDate = x.RebateDate,
                    RebateStatus = L(x.RebateStatus.ToString()),
                    UserName = x.UserName
                }).ToList();
            }
            catch (Exception ex)
            {
                vm.customActionMessage = ex.Message;
                vm.customActionStatus = "";
            }

            if (vm.data == null)
            {
                vm.data = new List<object>();
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}