using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.FAQ.Dto
{
    [AutoMapFrom(typeof(FAQCategory))]
    public class FAQCategoryDto:EntityDto
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public string FontIcon { get; set; }

        public bool IsEnable { get; set; }

        public int Sort { get; set; }

        public bool IsDeleted { get; set; }

        public IList<FAQDetailDto> Details { get; set; } 

    }
}
