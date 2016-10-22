using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_RefundRecord")]
    public class RefundRecord : Entity, ISoftDelete, IAudited
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int TicketOrderId { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public ApplyStatus ApplyStatus { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        public RefundStatus RefundStatus { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
