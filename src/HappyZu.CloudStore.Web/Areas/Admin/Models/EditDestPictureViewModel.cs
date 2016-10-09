using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.FileManager.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class EditDestPictureViewModel
    {
        public int Id { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsDefault { get; set; }
        public FileItemDto FileItem { get; set; }
    }
}