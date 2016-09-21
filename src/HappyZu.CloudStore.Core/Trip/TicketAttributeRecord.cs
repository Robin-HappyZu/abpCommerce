using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    [Table("Trip_TicketAttribute_Record")]
    public class TicketAttributeRecord : Entity
    {
        public int TicketId { get; set; }

        public int TicketAttributeId { get; set; }

        public string TicketAttributeName { get; set; }
    }
}
