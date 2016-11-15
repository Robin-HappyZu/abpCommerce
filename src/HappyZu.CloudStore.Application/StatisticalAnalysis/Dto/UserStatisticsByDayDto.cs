using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.StatisticalAnalysis.Dto
{
    [AutoMap(typeof(UserStatisticsByDay))]
    public class UserStatisticsByDayDto : EntityDto
    {
        public int Increase { get; set; }

        public int Total { get; set; }

        public int Day { get; set; }

        public DateTime Date { get; set; }
    }
}
