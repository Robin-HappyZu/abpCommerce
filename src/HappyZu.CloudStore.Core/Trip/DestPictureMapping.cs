using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_Dest_Picture_Mapping")]
    public class DestPictureMapping : Entity
    {
        public int DestId { get; set; }

        public int FileId { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsDefault { get; set; }
    }
}
