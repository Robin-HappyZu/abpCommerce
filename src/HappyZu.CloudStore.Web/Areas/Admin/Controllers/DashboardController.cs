using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
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
    }
}