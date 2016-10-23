using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(CustomizeTrip))]
    public class CustomizeTripDto : EntityDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long CustomerId { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public string Depart { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }

        public string Contact { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public string Remark { get; set; }
    }
}
