using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Entities
{
    public enum OrderStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [Display(Name = "待处理")]
        Pending,
        /// <summary>
        /// 待支付
        /// </summary>
        [Display(Name = "待支付")]
        Paying,
        /// <summary>
        /// 已支付
        /// </summary>
        [Display(Name = "已支付")]
        Paid,
        /// <summary>
        /// 已关闭
        /// </summary>
        [Display(Name = "已关闭")]
        Closed,
        /// <summary>
        /// 申请退款
        /// </summary>
        [Display(Name = "申请退款")]
        ApplyReturn,
        /// <summary>
        /// 已退款
        /// </summary>
        [Display(Name = "已退款")]
        Returned,
        /// <summary>
        /// 已完成
        /// </summary>
        [Display(Name = "已完成")]
        Completed
    }
}
