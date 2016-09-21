using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_DestAttribute_Record")]
    public class DestAttributeRecord : Entity
    {
        public int DestId { get; set; }

        public int DestAttributeId { get; set; }

        public string DestAttributeName { get; set; }
    }
}
