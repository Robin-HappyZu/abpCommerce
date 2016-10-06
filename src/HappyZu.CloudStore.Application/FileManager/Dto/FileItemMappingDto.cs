using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.FileManager.Dto
{
    public class FileItemMappingDto :EntityDto
    {
        public FileItemDto FileItem { get; set; }

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }
    }
}
