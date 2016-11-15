using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.StatisticalAnalysis.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public SalesStatisticsByDayDto SalesStatisticsByDay { get; set; }

        public UserStatisticsByDayDto UserStatisticsByDay { get; set; }
    }
}