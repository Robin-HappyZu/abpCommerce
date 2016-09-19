using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetPagedTicketsInput : IPagedResultRequest
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public int DestId { get; set; }
    }
}
