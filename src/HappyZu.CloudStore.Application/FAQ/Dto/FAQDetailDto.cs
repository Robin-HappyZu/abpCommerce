using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.FAQ.Dto
{
    [AutoMapFrom(typeof (FAQDetail))]
    public class FAQDetailDto:EntityDto
    {

    }
}
