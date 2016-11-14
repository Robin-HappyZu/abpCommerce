using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class SetUserRoleViewModel
    {
        public long UserId { get; set; }

        public IList<RoleViewModel> Roles { get; set; }
    }
}