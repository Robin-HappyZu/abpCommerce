using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(TicketOrderItem))]
    public class TicketOrderItemDto : EntityDto
    {
        /// <summary>
        /// TicketOrderItem编码
        /// </summary>
        public string TicketOrderItemNo { get; set; }

        /// <summary>
        /// 门票订单ID
        /// </summary>
        public int TicketOrderId { get; set; }

        /// <summary>
        /// 门票ID    
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Price { get; set; }

    }
}
