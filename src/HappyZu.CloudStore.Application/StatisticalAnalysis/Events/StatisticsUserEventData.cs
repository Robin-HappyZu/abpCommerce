using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace HappyZu.CloudStore.StatisticalAnalysis.Events
{
    public class StatisticsUserEventData : EventData
    {
        public long UserId { get; set; }
    }
}
