using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMapFrom(typeof(DestPictureMapping))]
    public class DestPictureMappingDto:EntityDto
    {
        public int DestId { get; set; }

        public int FileId { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsDefault { get; set; }
    }
}
