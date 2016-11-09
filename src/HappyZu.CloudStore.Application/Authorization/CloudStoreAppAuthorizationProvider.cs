using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Localization;
using Abp.Localization.Sources;

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
            administrator.CreateChildPermission(PermissionNames.Administrator_UserRestPassword);
            administrator.CreateChildPermission(PermissionNames.Administrator_UserActive);
            administrator.CreateChildPermission(PermissionNames.Administrator_UserRemove);
            var agents = context.CreatePermission(PermissionNames.Agents);
        }
    }
    
}
