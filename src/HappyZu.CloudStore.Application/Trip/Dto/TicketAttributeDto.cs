using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(TicketAttribute))]
    public class TicketAttributeDto : EntityDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}
