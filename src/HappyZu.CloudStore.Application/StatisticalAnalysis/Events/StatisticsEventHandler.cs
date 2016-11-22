using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Extensions;

namespace HappyZu.CloudStore.StatisticalAnalysis.Events
{
    public class StatisticsEventHandler:
        IEventHandler<StatisticsSalesEventData>,
        IEventHandler<StatisticsUserEventData>, ITransientDependency
    {
        private readonly UserStatisticsManager _userStatisticsManager;
        private readonly SalesStatisticsManager _salesStatisticsManager;

        public StatisticsEventHandler(UserStatisticsManager userStatisticsManager, SalesStatisticsManager salesStatisticsManager)
        {
            _userStatisticsManager = userStatisticsManager;
            _salesStatisticsManager = salesStatisticsManager;
        }

        public async void HandleEvent(StatisticsSalesEventData eventData)
        {
            #region 更新日期统计

            var etime = eventData.EventTime;
            var day =await _salesStatisticsManager.GetByDayAsync(etime.Date);
            if (day==null)
            {
                day = new SalesStatisticsByDay
                {
                    Day = etime.DayOfYear,
                    Date = etime.Date
                };
            }
            day.Subtotal += eventData.PaidAmount;
            day.Total += eventData.Total;
            day.Orders++;
            if (eventData.AgentId>0)
            {
                day.AgentOrders++;
            }
            await _salesStatisticsManager.UpdateDay(day);
            #endregion

            #region 更新周统计

            var weekOfYear = (etime.DayOfYear +
                              (int) new DateTime(etime.Year, 1, 1).DayOfWeek)/7;

            var firstDayOfWeek = etime.AddDays(-(int)etime.DayOfWeek).Date;

            var week = await _salesStatisticsManager.GetByWeekAsync(weekOfYear, firstDayOfWeek);
            if (week == null)
            {
                week = new SalesStatisticsByWeek
                {
                    Week = weekOfYear,
                    FirstDayOfWeek = firstDayOfWeek
                };
            }
            week.Subtotal += eventData.PaidAmount;
            week.Total += eventData.Total;
            week.Orders++;
            if (eventData.AgentId > 0)
            {
                week.AgentOrders++;
            }
            await _salesStatisticsManager.UpdateWeek(week);
            #endregion

            #region 更新月统计
            
            var firstDayOfMonth = new DateTime(etime.Year, etime.Month, 1);

            var month = await _salesStatisticsManager.GetByMonthAsync(etime.Month, firstDayOfMonth);
            if (month == null)
            {
                month = new SalesStatisticsByMonth
                {
                    Month = etime.Month,
                    FirstDayOfMonth = firstDayOfMonth
                };
            }
            month.Subtotal += eventData.PaidAmount;
            month.Total += eventData.Total;
            month.Orders++;
            if (eventData.AgentId > 0)
            {
                month.AgentOrders++;
            }
            await _salesStatisticsManager.UpdateMonth(month);
            #endregion
            
            #region 更新季度统计

            var quarterT = etime.Month/4 + 1;
            var firstDayOfQuarter = new DateTime(etime.Year, (quarterT - 1)*4, 1);

            var quarter = await _salesStatisticsManager.GetByQuarterAsync(quarterT, firstDayOfQuarter);
            if (quarter == null)
            {
                quarter = new SalesStatisticsByQuarter
                {
                    Quarter = quarterT,
                    FirstDayOfQuarter = firstDayOfQuarter
                };
            }
            quarter.Subtotal += eventData.PaidAmount;
            quarter.Total += eventData.Total;
            quarter.Orders++;
            if (eventData.AgentId > 0)
            {
                quarter.AgentOrders++;
            }
            await _salesStatisticsManager.UpdateQuarter(quarter);
            #endregion
        }

        public async void HandleEvent(StatisticsUserEventData eventData)
        {
            #region 更新日期统计

            var etime = eventData.EventTime;
            var day = await _userStatisticsManager.GetByDayAsync(etime.Date);
            if (day == null)
            {
                day = new UserStatisticsByDay
                {
                    Day = etime.DayOfYear,
                    Date = etime.Date
                };
            }
            day.Total ++;
            day.Increase++;

            await _userStatisticsManager.UpdateDay(day);
            #endregion

            #region 更新周统计

            var weekOfYear = (etime.DayOfYear +
                              (int)new DateTime(etime.Year, 1, 1).DayOfWeek) / 7;

            var firstDayOfWeek = etime.AddDays(-(int)etime.DayOfWeek).Date;

            var week = await _userStatisticsManager.GetByWeekAsync(weekOfYear, firstDayOfWeek);
            if (week == null)
            {
                week = new UserStatisticsByWeek
                {
                    Week = weekOfYear,
                    FirstDayOfWeek = firstDayOfWeek
                };
            }

            day.Total++;
            day.Increase++;

            await _userStatisticsManager.UpdateWeek(week);
            #endregion

            #region 更新月统计

            var firstDayOfMonth = new DateTime(etime.Year, etime.Month, 1);

            var month = await _userStatisticsManager.GetByMonthAsync(etime.Month, firstDayOfMonth);
            if (month == null)
            {
                month = new UserStatisticsByMonth
                {
                    Month = etime.Month,
                    FirstDayOfMonth = firstDayOfMonth
                };
            }

            day.Total++;
            day.Increase++;
            await _userStatisticsManager.UpdateMonth(month);
            #endregion

            #region 更新季度统计

            var quarterT = etime.Month / 4 + 1;
            var firstDayOfQuarter = new DateTime(etime.Year, (quarterT - 1) * 4, 1);

            var quarter = await _userStatisticsManager.GetByQuarterAsync(quarterT, firstDayOfQuarter);
            if (quarter == null)
            {
                quarter = new UserStatisticsByQuarter
                {
                    Quarter = quarterT,
                    FirstDayOfQuarter = firstDayOfQuarter
                };
            }
            day.Total++;
            day.Increase++;
            await _userStatisticsManager.UpdateQuarter(quarter);
            #endregion
        }
    }
}
