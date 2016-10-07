using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.FileManager
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
