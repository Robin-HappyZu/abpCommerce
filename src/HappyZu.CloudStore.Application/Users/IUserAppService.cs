using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Users.Dto;

namespace HappyZu.CloudStore.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task SetUnsubscribe(string openId);

        Task SetUnsubscribe(long userId);

        Task SetSubscribe(string openId);

        Task SetSubscribe(long userId);

        Task<ListResultDto<UserDto>> GetUsers();

        Task CreateUserAsync(CreateUserInput input);

        Task AddUserAsync(CreateUserInput input);

        Task AddUserLoginAsync(UserLoginInput input);

        Task<ClaimsIdentity> CreateIdentityAsync(User user);

        Task<UserDto> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId);

        Task<AbpLoginResult<Tenant, User>> UserLoginAsync(UserLoginInput input);
    }
}