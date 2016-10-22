using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models
{
    public class ConfirmTicketOrderViewModel
    {
        public List<CreateTicketOrderDto> Tickets { get; set; }

        public string Contact { get; set; }

        public string Mobile { get; set; }

        public string Remark { get; set; }
    }
}