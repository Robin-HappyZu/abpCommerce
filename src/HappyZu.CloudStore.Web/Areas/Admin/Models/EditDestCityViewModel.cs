using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class EditDestCityViewModel
    {
        public EditDestCityViewModel()
        {
            Province=new DestProvinceDto();
            City=new DestCityDto();
        }
        public int CityId { get; set; }

        public int ProvinceId { get; set; }
        public DestProvinceDto Province { get; set; }

        public DestCityDto City { get; set; }
    }
}