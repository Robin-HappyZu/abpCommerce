using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    [Table("Statistics_User_ByDay")]
    public class UserStatisticsByDay:Entity
    {
        public int Increase { get; set; }

        public int Total { get; set; }

        public int Day { get; set; }

        public DateTime Date { get; set; }
    }
}
