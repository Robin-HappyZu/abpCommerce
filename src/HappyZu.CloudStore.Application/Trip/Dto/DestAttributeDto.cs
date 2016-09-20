using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(DestAttribute))]
    public class DestAttributeDto : EntityDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}
