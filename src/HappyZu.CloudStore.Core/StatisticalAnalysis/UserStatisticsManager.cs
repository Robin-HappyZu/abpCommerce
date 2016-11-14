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
    public class UserStatisticsManager : IDomainService
    {
        private readonly IRepository<UserStatisticsByDay> _userStatisticsByDay;
        private readonly IRepository<UserStatisticsByWeek> _userStatisticsByWeek;
        private readonly IRepository<UserStatisticsByMonth> _userStatisticsByMonth;
        private readonly IRepository<UserStatisticsByQuarter> _userStatisticsByQuarter;

        public UserStatisticsManager(IRepository<UserStatisticsByDay> userStatisticsByDay, 
            IRepository<UserStatisticsByWeek> userStatisticsByWeek, 
            IRepository<UserStatisticsByMonth> userStatisticsByMonth, 
            IRepository<UserStatisticsByQuarter> userStatisticsByQuarter)
        {
            _userStatisticsByDay = userStatisticsByDay;
            _userStatisticsByWeek = userStatisticsByWeek;
            _userStatisticsByMonth = userStatisticsByMonth;
            _userStatisticsByQuarter = userStatisticsByQuarter;
        }

        #region ByDay
        public async Task<UserStatisticsByDay> UpdateDay(UserStatisticsByDay entity)
        {
            return await _userStatisticsByDay.InsertOrUpdateAsync(entity);
        }

        public async Task<UserStatisticsByDay> GetDayByIdAsync(int id)
        {
            return await _userStatisticsByDay.GetAsync(id);
        }

        public async Task<UserStatisticsByDay> GetByDayAsync(DateTime date)
        {
            return await _userStatisticsByDay.FirstOrDefaultAsync(q=>q.Date==date);
        }

        public Task<IReadOnlyList<UserStatisticsByDay>> QueryListByDayAsync(Func<IQueryable<UserStatisticsByDay>, IQueryable<UserStatisticsByDay>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _userStatisticsByDay.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _userStatisticsByDay.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<UserStatisticsByDay>)list);
        }
        
        public Task<int> QueryCountByDayAsync(Func<IQueryable<UserStatisticsByDay>, IQueryable<UserStatisticsByDay>> query)
        {
            var count = query != null ?
                    _userStatisticsByDay.Query(query).Count() :
                    _userStatisticsByDay.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByWeek
        public async Task<UserStatisticsByWeek> UpdateWeek(UserStatisticsByWeek entity)
        {
            return await _userStatisticsByWeek.InsertOrUpdateAsync(entity);
        }

        public async Task<UserStatisticsByWeek> GetWeekByIdAsync(int id)
        {
            return await _userStatisticsByWeek.GetAsync(id);
        }

        public async Task<UserStatisticsByWeek> GetByWeekAsync(int week,DateTime date)
        {
            return await _userStatisticsByWeek.FirstOrDefaultAsync(q => q.FirstDayOfWeek == date && q.Week==week);
        }

        public Task<IReadOnlyList<UserStatisticsByWeek>> QueryListByWeekAsync(Func<IQueryable<UserStatisticsByWeek>, IQueryable<UserStatisticsByWeek>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _userStatisticsByWeek.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _userStatisticsByWeek.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<UserStatisticsByWeek>)list);
        }

        public Task<int> QueryCountByWeekAsync(Func<IQueryable<UserStatisticsByWeek>, IQueryable<UserStatisticsByWeek>> query)
        {
            var count = query != null ?
                    _userStatisticsByWeek.Query(query).Count() :
                    _userStatisticsByWeek.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByMonth

        public async Task<UserStatisticsByMonth> UpdateMonth(UserStatisticsByMonth entity)
        {
            return await _userStatisticsByMonth.InsertOrUpdateAsync(entity);
        }

        public async Task<UserStatisticsByMonth> GetMonthByIdAsync(int id)
        {
            return await _userStatisticsByMonth.GetAsync(id);
        }

        public async Task<UserStatisticsByMonth> GetByMonthAsync(int month,DateTime date)
        {
            return await _userStatisticsByMonth.FirstOrDefaultAsync(q => q.FirstDayOfMonth == date && q.Month==month);
        }

        public Task<IReadOnlyList<UserStatisticsByMonth>> QueryListByMonthAsync(Func<IQueryable<UserStatisticsByMonth>, IQueryable<UserStatisticsByMonth>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _userStatisticsByMonth.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _userStatisticsByMonth.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<UserStatisticsByMonth>)list);
        }

        public Task<int> QueryCountByMonthAsync(Func<IQueryable<UserStatisticsByMonth>, IQueryable<UserStatisticsByMonth>> query)
        {
            var count = query != null ?
                    _userStatisticsByMonth.Query(query).Count() :
                    _userStatisticsByMonth.Count();

            return Task.FromResult(count);
        }
        #endregion

        #region ByQuarter

        public async Task<UserStatisticsByQuarter> UpdateQuarter(UserStatisticsByQuarter entity)
        {
            return await _userStatisticsByQuarter.InsertOrUpdateAsync(entity);
        }

        public async Task<UserStatisticsByQuarter> GetQuarterByIdAsync(int id)
        {
            return await _userStatisticsByQuarter.GetAsync(id);
        }

        public async Task<UserStatisticsByQuarter> GetByQuarterAsync(int quarter, DateTime date)
        {
            return await _userStatisticsByQuarter.FirstOrDefaultAsync(q => q.FirstDayOfQuarter == date && q.Quarter == quarter);
        }

        public Task<IReadOnlyList<UserStatisticsByQuarter>> QueryListByQuarterAsync(Func<IQueryable<UserStatisticsByQuarter>, IQueryable<UserStatisticsByQuarter>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _userStatisticsByQuarter.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _userStatisticsByQuarter.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<UserStatisticsByQuarter>)list);
        }

        public Task<int> QueryCountByQuarterAsync(Func<IQueryable<UserStatisticsByQuarter>, IQueryable<UserStatisticsByQuarter>> query)
        {
            var count = query != null ?
                    _userStatisticsByQuarter.Query(query).Count() :
                    _userStatisticsByQuarter.Count();

            return Task.FromResult(count);
        }
        #endregion


    }
}
