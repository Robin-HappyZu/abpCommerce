using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.FileManager.Dto
{
    [AutoMap(typeof(FileItem))]
    public class FileItemDto :EntityDto
    {
        public string Path { get; set; }

        public string MimeType { get; set; }

        public string SEOFileName { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }
    }
}
