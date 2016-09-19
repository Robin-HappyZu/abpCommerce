using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class AddDestProvinceInput
    {
        public AddDestProvinceInput()
        {
            Province=new DestProvinceDto();
        }
        public DestProvinceDto Province { get; set; }
    }
}
