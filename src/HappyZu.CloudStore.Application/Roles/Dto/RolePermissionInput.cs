using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Roles.Dto
{
    public class RolePermissionInput
    {
        public int RoleId { get; set; }

        public IList<string> Permissions { get; set; }
    }
}
