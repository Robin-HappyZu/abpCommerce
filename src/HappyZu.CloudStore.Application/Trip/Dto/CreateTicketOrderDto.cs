using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class CreateTicketOrderDto
    {
        /// <summary>
        /// 门票ID
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// 报价ID
        /// </summary>
        public int TicketQuoteId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
