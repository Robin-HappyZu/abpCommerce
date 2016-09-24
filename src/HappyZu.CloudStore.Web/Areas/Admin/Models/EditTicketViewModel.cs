using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class EditTicketViewModel
    {
        public EditTicketViewModel()
        {
            Dest=new DestDto();
        }

        /// <summary>
        /// 景点信息
        /// </summary>
        public DestDto Dest { get; set; }
    }
}