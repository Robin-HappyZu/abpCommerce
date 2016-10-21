using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.Dest
{
    public class GetQuotesViewModel
    {
        public int Id { get; set; }
        public DateTime? MaxDate { get; set; }

        public DateTime? MinDate { get; set; }
    }
}