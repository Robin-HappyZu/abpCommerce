using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetDestProvincesInput :IPagedResultRequest
    {
        public int CountryType { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
    }
}
