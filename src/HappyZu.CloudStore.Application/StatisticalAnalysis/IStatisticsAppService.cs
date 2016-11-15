using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.StatisticalAnalysis.Dto;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    public interface IStatisticsAppService : IApplicationService
    {
        #region 销售统计数据

        Task<SalesStatisticsByDayDto> GetSalesStatisticsByDay(DateTime date);

        Task<IPagedResult<SalesStatisticsByMonthDto>> QuerySalesStatisticsByMonth(QuerySalesStatisticsByMonthInput input);

        #endregion

        #region 用户统计数据
        Task<UserStatisticsByDayDto> GetUserStatisticsByDay(DateTime date);
        
        #endregion
    }
}
