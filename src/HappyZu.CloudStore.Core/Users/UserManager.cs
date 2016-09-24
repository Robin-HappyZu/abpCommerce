using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.MultiTenancy;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Users
{
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository
            )
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                userLoginAttemptRepository)
        {
        }

        public async Task AddLoginAsync(User user, UserLoginInfo login)
        {
            await AbpStore.AddLoginAsync(user, login);
        }

        public async Task AddUserAsync(User user)
        {
            user.Roles = new List<UserRole>();
            foreach (var defaultRole in RoleManager.Roles.Where(r => r.TenantId == user.TenantId && r.IsDefault).ToList())
            {
                user.Roles.Add(new UserRole(user.TenantId, user.Id, defaultRole.Id));
            }

            await Store.CreateAsync(user);
        }

        public async Task<User> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId)
        {
            var userStore = AbpStore as UserStore;
            if (userStore == null)
            {
                return null;
            }
            return await userStore?.GetUserByWechatOpenIdAndUnionIdAsync(wechatOpenId, unionId);
        }
    }
}