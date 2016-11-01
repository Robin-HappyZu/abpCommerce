using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace HappyZu.CloudStore.Wechat.Events
{
    public class SubscribeEventData : EventData
    {
        public string OpenId { get; set; }

        public long UserId { get; set; }
    }
}
