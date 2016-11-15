using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.StatisticalAnalysis.Dto
{
    [AutoMap(typeof(SalesStatisticsByDay))]
    public class SalesStatisticsByDayDto : EntityDto
    {
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public int Orders { get; set; }

        public int AgentOrders { get; set; }

        public int Day { get; set; }

        public DateTime Date { get; set; }
    }
}
