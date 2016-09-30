using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using HappyZu.CloudStore.Authorization;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Users.Dto;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Users
{
    public class UserAppService : CloudStoreAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly AbpLogInManager<Tenant, Role, User> _logInManager; 

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager, AbpLogInManager<Tenant, Role, User> logInManager)   
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _logInManager = logInManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserDto>(
                users.MapTo<List<UserDto>>()
                );
        }

        public async Task CreateUserAsync(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            using (var uow = UnitOfWorkManager.Begin())
            {
                await UnitOfWorkManager.Current.SaveChangesAsync();
                uow.Complete();
            }
            await UserManager.CreateAsync(user);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task AddUserLoginAsync(UserLoginInput input)
        {
            var user = input.User.MapTo<User>();
            await UserManager.AddLoginAsync(user, new UserLoginInfo(input.LoginProvider, input.ProviderKey));
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user)
        {
            return await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<UserDto> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId)
        {
            var user = await UserManager.GetUserByWechatOpenIdAndUnionIdAsync(wechatOpenId, unionId);
            return user.MapTo<UserDto>();
        }

        public async Task<AbpLoginResult<Tenant, User>> UserLoginAsync(UserLoginInput input)
        {
            var login = new UserLoginInfo(input.LoginProvider, input.ProviderKey);
            var loginResult = await _logInManager.LoginAsync(login);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw new Exception("Login Failed");
            }
        }
    }
}