using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace HappyZu.CloudStore.Trip.Events
{
    public class OrderPaidEventData : EventData
    {
        public string TradeNo { get; set; }
        public string TransactionNo { get; set; }
        public decimal Amount { get; set; }
    }
}
