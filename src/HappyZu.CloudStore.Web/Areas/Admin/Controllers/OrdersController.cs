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
    public class OrdersController : Controller
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
            return Json(null);
        }
        #endregion
    }
}