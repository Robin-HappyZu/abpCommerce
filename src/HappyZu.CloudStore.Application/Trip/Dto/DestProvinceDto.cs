using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMapFrom(typeof(DestProvince))]
    public class DestProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public CountryType DestType { get; set; }
        public bool IsDeleted { get; set; }
    }
}

