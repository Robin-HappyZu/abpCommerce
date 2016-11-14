using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

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


        #endregion

        #region ByWeek
        public async Task<UserStatisticsByWeek> UpdateWeek(UserStatisticsByWeek entity)
        {
            return await _userStatisticsByWeek.InsertOrUpdateAsync(entity);
        }

        #endregion

        #region ByMonth
        
        public async Task<UserStatisticsByMonth> UpdateMonth(UserStatisticsByMonth entity)
        {
            return await _userStatisticsByMonth.InsertOrUpdateAsync(entity);
        }

        #endregion

        #region ByQuarter

        public async Task<UserStatisticsByQuarter> UpdateQuarter(UserStatisticsByQuarter entity)
        {
            return await _userStatisticsByQuarter.InsertOrUpdateAsync(entity);
        }
        #endregion


    }
}
