using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Authorization;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class PermissionViewModel
    {
        public IReadOnlyList<Permission> Permissions { get; set; }

        public IReadOnlyList<RoleViewModel> Roles { get; set; }
    }
}