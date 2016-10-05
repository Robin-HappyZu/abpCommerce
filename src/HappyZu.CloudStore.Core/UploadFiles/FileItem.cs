using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.UploadFiles
{
    [Table("UploadFile_FileItem")]
    public class FileItem : Entity
    {
        [StringLength(255)]
        public string Path { get; set; }

        [StringLength(50)]
        public string MimeType { get; set; }

        [StringLength(255)]
        public string SEOFileName { get; set; }

        [StringLength(255)]
        public string AltAttribute { get; set; }

        [StringLength(255)]
        public string TitleAttribute { get; set; }
    }
}
