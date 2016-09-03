using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_City")]
    public class DestCity : Entity, ISoftDelete
    {
        [StringLength(255)]
        public string Name { get; set; }
        
        public int ProvinceId { get; set; }
        
        [ForeignKey("ProvinceId")]
        public DestProvince Province { get; set; }

        public int DestCount { get; set; }

        public bool IsDeleted { get; set; }
    }
}
