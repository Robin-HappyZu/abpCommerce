using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;

namespace HappyZu.CloudStore.Authorization
{
    public class CloudStoreAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var administrator = context.CreatePermission("Administrator");
            administrator.CreateChildPermission("Administrator.UserManager");
            administrator.CreateChildPermission("Administrator.RoleManager");
            administrator.CreateChildPermission("Administrator.UserRoleManager");
        }
    }
}
