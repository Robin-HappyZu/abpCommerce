using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Roles.Dto;

namespace HappyZu.CloudStore.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);

        #region 用户组操作
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetRolesAsync();

        Task<PagedResultDto<RoleDto>> GetRolesAsync(GetRolesInput input);

        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="roleInputDto"></param>
        /// <returns></returns>
        Task<ResultOutputDto> CreateRoleAsync(RoleInput roleInputDto);

        /// <summary>
        /// 编辑用户组
        /// </summary>
        /// <param name="roleInputDto"></param>
        /// <returns></returns>
        Task<ResultOutputDto> EditRoleAsync(RoleInput roleInputDto);

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="roleInputDto"></param>
        /// <returns></returns>
        Task<ResultOutputDto> RemoveRoleAsync(RoleInput roleInputDto);

        /// <summary>
        /// 根据用户组ID删除用户组
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ResultOutputDto> RemoveRoleByIdAsync(int roleId);

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<GetRoleOutput> GetRoleByIdAsync(int roleId);

        #endregion

        #region 用户组权限
        /// <summary>
        /// 设置用户组权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResultOutputDto> SetRolePermissionsAsync(RolePermissionInput input);

        /// <summary>
        /// 删除用户组权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResultOutputDto> RemovePermissionAsync(RemoveRolePermissionInput input);

        /// <summary>
        /// 清空用户组权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ResultOutputDto> ClearRolePermissionAsync(int roleId);

        /// <summary>
        /// 获取用户组所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<RolePermissionOutputDto> GetRolePermissionsAsync(int roleId);
        #endregion
    }
}
