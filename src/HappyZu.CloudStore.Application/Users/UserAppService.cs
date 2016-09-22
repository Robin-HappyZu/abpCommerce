using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using HappyZu.CloudStore.Authorization;
using HappyZu.CloudStore.Users.Dto;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : CloudStoreAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
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

        public async Task<ListResultOutput<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultOutput<UserDto>(
                users.MapTo<List<UserDto>>()
                );
        }

        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await UserManager.CreateAsync(user));
        }

        public async Task<UserDto> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId)
        {
            var result = await _userRepository.SingleAsync(user => user.WechatOpenID == wechatOpenId && user.UnionID == unionId);

            return result.MapTo<UserDto>();
        }
    }
}