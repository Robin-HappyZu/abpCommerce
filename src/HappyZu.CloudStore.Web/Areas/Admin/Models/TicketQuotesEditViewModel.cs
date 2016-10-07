using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class TicketQuotesEditViewModel
    {
        public int TicketId { get; set; }

        public int QuotesType { get; set; }
        

        public IList<TicketQuoteDto> TicketQuotes { get; set; } 
    }
    
}