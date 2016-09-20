using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class DestCityViewModel
    {
        public DestCityViewModel()
        {
            Province=new DestProvinceDto();
        }
        public DestProvinceDto Province { get; set; }
    }
}