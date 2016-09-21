using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_TicketOrderItem")]
    public class TicketOrderItem : Entity
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
