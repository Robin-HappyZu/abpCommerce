using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
using Happyzu.CloudStore.Extensions;
using HappyZu.CloudStore.StatisticalAnalysis;
using HappyZu.CloudStore.StatisticalAnalysis.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    [AbpAuthorize]
    public class DashboardController : AdminControllerBase
    {
        private readonly IStatisticsAppService _statisticsAppService;

        public DashboardController(IStatisticsAppService statisticsAppService)
        {
            _statisticsAppService = statisticsAppService;
        }

        // GET: Admin/Dashboard
        public async Task<ActionResult> Index()
        {
            var sales =await _statisticsAppService.GetSalesStatisticsByDay(DateTime.Now.Date);
            var users = await _statisticsAppService.GetUserStatisticsByDay(DateTime.Now.Date);
            if (sales==null)
            {
                sales = new SalesStatisticsByDayDto();
            }
            if (users==null)
            {
                users = new UserStatisticsByDayDto();
            }
            var vm=new DashboardViewModel()
            {
                SalesStatisticsByDay = sales,
                UserStatisticsByDay = users
            };
            return View(vm);
        }

        public async Task<JsonResult> GetDashboardOrderStatistics(int count)
        {
            Dictionary<string, int> dataOrders = new Dictionary<string, int>();
            Dictionary<string, decimal> dataAmount = new Dictionary<string, decimal>();
            var list = await _statisticsAppService.QuerySalesStatisticsByMonth(new QuerySalesStatisticsByMonthInput
            {
                MaxResultCount = count
            });
            for (int i = count; i >= 1; i--)
            {
                var startTime = DateTime.Now.ToMonthStart().AddMonths(-i + 1);

                var key = startTime.ToString("yyyy-MM");

                var value = list.Items.FirstOrDefault(x => x.FirstDayOfMonth == startTime);
                if (value!=null)
                {
                    dataOrders.Add(key, value.Orders);
                    dataAmount.Add(key, value.Subtotal);
                }
                else
                {
                    dataOrders.Add(key, 0);
                    dataAmount.Add(key, 0);
                }
            }
            return Json(new {orders= dataOrders ,amounts= dataAmount }, JsonRequestBehavior.AllowGet);
        }
    }
}