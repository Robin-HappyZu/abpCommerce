using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    public enum RefundStatus
    {
        None,
        [Description("待处理")]
        Pendding,
        [Description("处理中")]
        Processing,
        [Description("已完成")]
        Completed
    }
}
