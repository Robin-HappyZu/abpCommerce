using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class UpdateDestCityInput
    {
        public int Id { get; set; }
        public DestCityDto City { get; set; }
    }
}
