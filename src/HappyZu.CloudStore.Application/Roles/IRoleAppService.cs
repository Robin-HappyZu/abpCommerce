using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Roles.Dto;

namespace HappyZu.CloudStore.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
