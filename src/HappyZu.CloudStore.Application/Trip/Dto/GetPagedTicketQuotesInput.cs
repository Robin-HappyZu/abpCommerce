using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetPagedTicketQuotesInput : IPagedResultRequest
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public int TicketId { get; set; }
        
        public DateTime? MaxDate { get; set; }

        public DateTime? MinDate { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }
    }
}
