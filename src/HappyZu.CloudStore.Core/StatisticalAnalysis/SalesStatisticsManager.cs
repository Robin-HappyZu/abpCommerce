using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;

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
        
        #region ByDay
        public async Task<SalesStatisticsByDay> UpdateDay(SalesStatisticsByDay entity)
        {
            return await _salesStatisticsByDay.InsertOrUpdateAsync(entity);
        }

        public async Task<SalesStatisticsByDay> GetDayByIdAsync(int id)
        {
            return await _salesStatisticsByDay.GetAsync(id);
        }

        public async Task<SalesStatisticsByDay> GetByDayAsync(DateTime date)
        {
            return await _salesStatisticsByDay.FirstOrDefaultAsync(q => q.Date == date);
        }

        public Task<IReadOnlyList<SalesStatisticsByDay>> QueryListByDayAsync(Func<IQueryable<SalesStatisticsByDay>, IQueryable<SalesStatisticsByDay>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _salesStatisticsByDay.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _salesStatisticsByDay.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<SalesStatisticsByDay>)list);
        }

        public Task<int> QueryCountByDayAsync(Func<IQueryable<SalesStatisticsByDay>, IQueryable<SalesStatisticsByDay>> query)
        {
            var count = query != null ?
                    _salesStatisticsByDay.Query(query).Count() :
                    _salesStatisticsByDay.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByWeek
        public async Task<SalesStatisticsByWeek> UpdateWeek(SalesStatisticsByWeek entity)
        {
            return await _salesStatisticsByWeek.InsertOrUpdateAsync(entity);
        }

        public async Task<SalesStatisticsByWeek> GetWeekByIdAsync(int id)
        {
            return await _salesStatisticsByWeek.GetAsync(id);
        }

        public async Task<SalesStatisticsByWeek> GetByWeekAsync(int week, DateTime date)
        {
            return await _salesStatisticsByWeek.FirstOrDefaultAsync(q => q.FirstDayOfWeek == date && q.Week == week);
        }

        public Task<IReadOnlyList<SalesStatisticsByWeek>> QueryListByWeekAsync(Func<IQueryable<SalesStatisticsByWeek>, IQueryable<SalesStatisticsByWeek>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _salesStatisticsByWeek.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _salesStatisticsByWeek.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<SalesStatisticsByWeek>)list);
        }

        public Task<int> QueryCountByWeekAsync(Func<IQueryable<SalesStatisticsByWeek>, IQueryable<SalesStatisticsByWeek>> query)
        {
            var count = query != null ?
                    _salesStatisticsByWeek.Query(query).Count() :
                    _salesStatisticsByWeek.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByMonth

        public async Task<SalesStatisticsByMonth> UpdateMonth(SalesStatisticsByMonth entity)
        {
            return await _salesStatisticsByMonth.InsertOrUpdateAsync(entity);
        }

        public async Task<SalesStatisticsByMonth> GetMonthByIdAsync(int id)
        {
            return await _salesStatisticsByMonth.GetAsync(id);
        }

        public async Task<SalesStatisticsByMonth> GetByMonthAsync(int month, DateTime date)
        {
            return await _salesStatisticsByMonth.FirstOrDefaultAsync(q => q.FirstDayOfMonth == date && q.Month == month);
        }

        public Task<IReadOnlyList<SalesStatisticsByMonth>> QueryListByMonthAsync(Func<IQueryable<SalesStatisticsByMonth>, IQueryable<SalesStatisticsByMonth>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _salesStatisticsByMonth.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _salesStatisticsByMonth.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<SalesStatisticsByMonth>)list);
        }

        public Task<int> QueryCountByMonthAsync(Func<IQueryable<SalesStatisticsByMonth>, IQueryable<SalesStatisticsByMonth>> query)
        {
            var count = query != null ?
                    _salesStatisticsByMonth.Query(query).Count() :
                    _salesStatisticsByMonth.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByQuarter

        public async Task<SalesStatisticsByQuarter> UpdateQuarter(SalesStatisticsByQuarter entity)
        {
            return await _salesStatisticsByQuarter.InsertOrUpdateAsync(entity);
        }

        public async Task<SalesStatisticsByQuarter> GetQuarterByIdAsync(int id)
        {
            return await _salesStatisticsByQuarter.GetAsync(id);
        }

        public async Task<SalesStatisticsByQuarter> GetByQuarterAsync(int quarter, DateTime date)
        {
            return await _salesStatisticsByQuarter.FirstOrDefaultAsync(q => q.FirstDayOfQuarter == date && q.Quarter == quarter);
        }

        public Task<IReadOnlyList<SalesStatisticsByQuarter>> QueryListByQuarterAsync(Func<IQueryable<SalesStatisticsByQuarter>, IQueryable<SalesStatisticsByQuarter>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _salesStatisticsByQuarter.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _salesStatisticsByQuarter.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<SalesStatisticsByQuarter>)list);
        }

        public Task<int> QueryCountByQuarterAsync(Func<IQueryable<SalesStatisticsByQuarter>, IQueryable<SalesStatisticsByQuarter>> query)
        {
            var count = query != null ?
                    _salesStatisticsByQuarter.Query(query).Count() :
                    _salesStatisticsByQuarter.Count();

            return Task.FromResult(count);
        }
        #endregion
    }
}
