using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMapFrom(typeof(DestCity))]
    public class DestCityDto : EntityDto
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public int DestCount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
