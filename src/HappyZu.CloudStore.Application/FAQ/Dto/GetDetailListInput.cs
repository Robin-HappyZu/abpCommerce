using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.FAQ.Dto
{
    public class GetDetailListInput:IPagedResultRequest
    {
        public int CategoryId { get; set; }
        
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
    }
}
