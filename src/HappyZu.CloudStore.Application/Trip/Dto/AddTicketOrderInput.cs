using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class AddTicketOrderInput
    {
        public List<CreateTicketOrderDto> Tickets { get; set; }

        public string Contact { get; set; }

        public string Mobile { get; set; }

        public string Remark { get; set; }

        public long? AgentId { get; set; }
    }
}
