using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Agents.Dto
{
    [AutoMap(typeof(Rebate))]
    public class RebateDto:EntityDto
    {
        public long AgentId { get; set; }

        public string OrderType { get; set; }
        
        public string OrderNo { get; set; }

        public string UserName { get; set; }

        public DateTime PaidTime { get; set; }

        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计收入
        /// </summary>
        public decimal RebateAmount { get; set; }

        /// <summary>
        /// 真实收入
        /// </summary>
        public decimal IncomeAmount { get; set; }

        public DateTime ExpectedRebateDate { get; set; }

        public DateTime RebateDate { get; set; }

        public RebateStatus RebateStatus { get; set; }
    }
}
