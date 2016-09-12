using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_Province")]
    public class DestProvince : Entity, ISoftDelete, IAudited
    {
        [StringLength(255)]
        public string Name { get; set; }

        public CountryType DestType { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
