using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    public class SalesStatisticsManager : IDomainService
    {
        private readonly IRepository<SalesStatisticsByDay> _salesStatisticsByDay;
        private readonly IRepository<SalesStatisticsByWeek> _salesStatisticsByWeek;
        private readonly IRepository<SalesStatisticsByMonth> _salesStatisticsByMonth;
        private readonly IRepository<SalesStatisticsByQuarter> _salesStatisticsByQuarter;

        public SalesStatisticsManager(IRepository<SalesStatisticsByDay> salesStatisticsByDay, 
            IRepository<SalesStatisticsByWeek> salesStatisticsByWeek, 
            IRepository<SalesStatisticsByMonth> salesStatisticsByMonth, 
            IRepository<SalesStatisticsByQuarter> salesStatisticsByQuarter)
        {
            _salesStatisticsByDay = salesStatisticsByDay;
            _salesStatisticsByWeek = salesStatisticsByWeek;
            _salesStatisticsByMonth = salesStatisticsByMonth;
            _salesStatisticsByQuarter = salesStatisticsByQuarter;
        }
    }
}
