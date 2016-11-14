using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.StatisticalAnalysis
{
    [Table("Statistics_Sales_ByDay")]
    public class SalesStatisticsByDay:Entity
    {
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public int Orders { get; set; }

        public int AgentOrders { get; set; }

        public int Day { get; set; }

        public DateTime Date { get; set; }
    }
}
