using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class GetETicketsViewModel : DataTableOptionViewModel
    {
        public long SerialNo { get; set; }
        public int TicketOrderId { get; set; }
    }
}