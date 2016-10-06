using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.FileManager.Dto
{
    public class GetPagedFileItemInput : IPagedResultRequest
    {
        /// <summary>
        /// 通用的关联Id //如：DestPictureMapping.DestId
        /// </summary>
        public int MappingId { get; set; }

        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }
    }
}
