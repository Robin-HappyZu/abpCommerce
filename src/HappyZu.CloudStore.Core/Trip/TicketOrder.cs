using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 门票订单
    /// </summary>
    [Table("Trip_TicketOrder")]
    public class TicketOrder : Entity, ISoftDelete, ICreationAudited
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(128)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 使用积分
        /// </summary>
        public int UsedPoint { get; set; }

        /// <summary>
        /// 总票数
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// 保险费
        /// </summary>
        public double InsurancePremium { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public double PaidAmount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(50)]
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(50)]
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 已核销
        /// </summary>
        public bool IsVerification { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? CreatorUserId { get; set; }
    }
}
