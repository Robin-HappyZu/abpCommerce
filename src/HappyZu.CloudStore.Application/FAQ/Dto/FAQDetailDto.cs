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
        public virtual string Title { get; set; }

        public virtual int CategoryId { get; set; }
        
        public virtual string CategoryName { get; set; }

        public virtual string Discription { get; set; }

        public virtual int Sort { get; set; }

        public bool IsDeleted { get; set; }
    }
}
