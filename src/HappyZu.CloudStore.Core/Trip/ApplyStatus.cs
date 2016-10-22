using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    public enum ApplyStatus
    {
        None,
        [Description("申请中")]
        Applying,
        [Description("已拒绝")]
        Denied,
        [Description("已批准")]
        Approved
    }
}
