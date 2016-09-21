using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(TicketAttributeRecord))]
    public class TicketAttributeRecordDto : EntityDto
    {
        public int TicketId { get; set; }

        public int TicketAttributeId { get; set; }

        public string TicketAttributeName { get; set; }
    }
}
