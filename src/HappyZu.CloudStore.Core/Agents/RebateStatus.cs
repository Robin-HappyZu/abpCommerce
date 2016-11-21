using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Agents
{
    public enum RebateStatus
    {
        [Display(Name ="未返")]
        Pending,
        [Display(Name = "全部已返")]
        AllPaid,
        [Display(Name = "部分已返")]
        PartPaid,
        [Display(Name = "退款不返")]
        NoPaid,

    }
}
