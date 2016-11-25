using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Agents.Dto
{
    public class QueryRebatesInput : IPagedResultRequest
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }

        public long AgentId { get; set; }

        public RebateStatus? RebateStatus { get; set; }

        public string UserName { get; set; }
    }
}
