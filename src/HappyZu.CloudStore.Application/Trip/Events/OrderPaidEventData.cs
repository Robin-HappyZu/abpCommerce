﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using HappyZu.CloudStore.Wechat.Dto;

namespace HappyZu.CloudStore.Trip.Events
{
    public class OrderPaidEventData : EventData
    {
        public WechatPayResult WechatPayResult { get; set; }
    }
}
