using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.StatisticalAnalysis.Dto;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    public class StatisticsAppService : IStatisticsAppService
    {
        private readonly SalesStatisticsManager _salesStatisticsManager;
        private readonly UserStatisticsManager _userStatisticsManager;

        public StatisticsAppService(SalesStatisticsManager salesStatisticsManager, UserStatisticsManager userStatisticsManager)
        {
            _salesStatisticsManager = salesStatisticsManager;
            _userStatisticsManager = userStatisticsManager;
        }

        public async Task<SalesStatisticsByDayDto> GetSalesStatisticsByDay(DateTime date)
        {
            try
            {
                var sales = await _salesStatisticsManager.GetByDayAsync(date);
                return sales?.MapTo<SalesStatisticsByDayDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IPagedResult<SalesStatisticsByMonthDto>> QuerySalesStatisticsByMonth(QuerySalesStatisticsByMonthInput input)
        {
            var list=await _salesStatisticsManager.QueryListByMonthAsync(q => q.OrderByDescending(x => x.FirstDayOfMonth), input);
            var count = await _salesStatisticsManager.QueryCountByMonthAsync(null);
            return new PagedResultDto<SalesStatisticsByMonthDto>()
            {
                TotalCount = count,
                Items = list.MapTo<IReadOnlyList<SalesStatisticsByMonthDto>>()
            };
        }

        public async Task<UserStatisticsByDayDto> GetUserStatisticsByDay(DateTime date)
        {
            try
            {
                var user = await _userStatisticsManager.GetByDayAsync(date);
                return user?.MapTo<UserStatisticsByDayDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
