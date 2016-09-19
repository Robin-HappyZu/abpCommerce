using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class UpdateDestProvinceInput
    {
        public int Id { get; set; }
        public DestProvinceDto Province { get; set; }
    }
}
