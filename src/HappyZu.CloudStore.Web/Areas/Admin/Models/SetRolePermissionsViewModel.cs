using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class SetRolePermissionsViewModel
    {
        public int RoleId { get; set; }

        public List<string> PermissionNames { get; set; }
    }
}