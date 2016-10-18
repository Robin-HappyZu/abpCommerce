using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.FileManager.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.Dest
{
    public class DetailViewModel
    {
        public DestDto Dest { get; set; }

        public IPagedResult<TicketDto> Tickets { get; set; }

        public IPagedResult<TicketTypeDto> TicketTypes { get; set; }
        public IPagedResult<FileItemMappingDto> Pictures { get; set; }

        public DestCityDto City { get; set; }

        public DestProvinceDto Province { get; set; }
    }
}