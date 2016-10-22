using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(TicketOrder))]
    public class TicketOrderDto : EntityDto
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string DestName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
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
        public decimal InsurancePremium { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
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
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// 代理ID
        /// </summary>
        public long? AgentId { get; set; }
    }
}
