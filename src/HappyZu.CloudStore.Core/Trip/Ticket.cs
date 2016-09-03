using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 门票
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// 景点Id
        /// </summary>
        public int DestId { get; set; }

        /// <summary>
        /// 门票类型Id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 门票类型
        /// </summary>
        [ForeignKey("TypeId")]
        public TicketType TicketType { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }

        /// <summary>
        /// 门票描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Points { get; set; }
    }
}
