using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 门票报价
    /// </summary>
    [Table("Trip_TicketQuotes")]
    public class TicketQuotes
    {
        /// <summary>
        /// 门票Id
        /// </summary>
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 报价
        /// </summary>
        public Quote Quote { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int Sales { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        public bool IsDisplay { get; set; }
    }
}
