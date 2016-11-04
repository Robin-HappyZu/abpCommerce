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
            var administrator = context.CreatePermission(PermissionNames.Administrator);
            administrator.CreateChildPermission(PermissionNames.Administrator_UserManager);
            administrator.CreateChildPermission(PermissionNames.Administrator_RoleManager);
            administrator.CreateChildPermission(PermissionNames.Administrator_UserRoleManager);

            var agents = context.CreatePermission(PermissionNames.Agents);
        }
    }
    
}
