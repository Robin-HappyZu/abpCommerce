using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Users.Dto
{
    public class UserLoginInput
    {
        public UserDto User { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
