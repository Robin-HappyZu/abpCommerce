using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Users.Dto;

namespace HappyZu.CloudStore.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultOutput<UserDto>> GetUsers();

        Task CreateUser(CreateUserInput input);

        Task<UserDto> GetUserByWechatOpenIdAndUnionIdAsync(string wechatOpenId, string unionId);
    }
}