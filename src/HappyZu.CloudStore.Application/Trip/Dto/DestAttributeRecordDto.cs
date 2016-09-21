using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(DestAttributeRecord))]
    public class DestAttributeRecordDto : EntityDto
    {
        public int DestId { get; set; }

        public int DestAttributeId { get; set; }

        public string DestAttributeName { get; set; }
    }
}
