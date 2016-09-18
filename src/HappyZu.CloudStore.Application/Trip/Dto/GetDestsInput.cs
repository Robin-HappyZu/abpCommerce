using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetDestsInput : IPagedResultRequest
    {
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
    }
}
