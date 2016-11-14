using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace HappyZu.CloudStore.StatisticalAnalysis.Events
{
    public class StatisticsSalesEventData : EventData
    {
        public int OrderId { get; set; }

        public decimal Total { get; set; }

        public int AgentId { get; set; }

        public decimal PaidAmount { get; set; }
    }
}
