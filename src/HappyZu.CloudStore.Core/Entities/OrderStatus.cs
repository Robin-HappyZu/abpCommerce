using System;
using System.Collections.Generic;
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
        Pending,
        /// <summary>
        /// 待支付
        /// </summary>
        Paying,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 申请退款
        /// </summary>
        ApplyReturn,
        /// <summary>
        /// 已退款
        /// </summary>
        Returned,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed
    }
}
