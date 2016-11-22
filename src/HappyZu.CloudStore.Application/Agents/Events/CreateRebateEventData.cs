using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace HappyZu.CloudStore.Agents.Events
{
    public class CreateRebateEventData : EventData
    {
        public long AgentId { get; set; }

        public string OrderType { get; set; }

        public int OrderId { get; set; }
        
    }
}
