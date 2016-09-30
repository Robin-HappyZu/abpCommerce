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
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.MultiTenancy;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                  userStore,
                  roleManager,
                  permissionManager,
                  unitOfWorkManager,
                  cacheManager,
                  organizationUnitRepository,
                  userOrganizationUnitRepository,
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor)
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