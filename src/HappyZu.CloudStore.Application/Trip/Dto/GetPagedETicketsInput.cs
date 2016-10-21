using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetPagedETicketsInput : IPagedResultRequest
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public long SerialNo { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int TicketOrderId { get; set; }
    }
}
