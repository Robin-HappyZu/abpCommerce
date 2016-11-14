using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    [Table("Statistics_User_ByWeek")]
    public class UserStatisticsByWeek : Entity
    {
        public int Increase { get; set; }

        public int Total { get; set; }
        
        public int Week { get; set; }

        public DateTime FirstDayOfWeek { get; set; }
    }
}
