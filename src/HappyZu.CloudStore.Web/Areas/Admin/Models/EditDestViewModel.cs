using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class EditDestViewModel
    {
        public EditDestViewModel()
        {
            Dest = new DestDto();
            TicketTypes=new List<TicketTypeDto>();
        }

        public int Id { get; set; }
        public DestDto Dest { get; set; }

        public IReadOnlyList<TicketTypeDto> TicketTypes { get; set; }

        public IReadOnlyList<TicketDto> Tickets { get; set; }
    }
}