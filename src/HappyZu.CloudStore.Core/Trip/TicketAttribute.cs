using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_TicketAttribute")]
    public class TicketAttribute : Entity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}
