using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Roles.Dto;

namespace HappyZu.CloudStore.Roles
{
    /* THIS IS JUST A SAMPLE. */
    public class RoleAppService : CloudStoreAppServiceBase,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;
        private readonly TenantManager _tenantManager;

        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager, TenantManager tenantManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _tenantManager = tenantManager;
        }

        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        public async Task<ListResultOutput<RoleDto>> GetRolesAsync()
        {
            var list= await _roleManager.GetAllListAsync();
            return new ListResultOutput<RoleDto>(list.MapTo<IReadOnlyList<RoleDto>>());
        }

        public async Task<PagedResultOutput<RoleDto>> GetRolesAsync(GetRolesInput input)
        {
            var count = await _roleManager.CountAsync();
            var roles = _roleManager.Roles.PageBy(input).ToList();
            return new PagedResultOutput<RoleDto>
            {
                TotalCount = count,
                Items = roles.MapTo<List<RoleDto>>()
            };
        }

        public async Task<ResultOutputDto> CreateRoleAsync(RoleInput roleInputDto)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(roleInputDto.TenantName);
            //TODO:tenant 是否为空
            var result = await _roleManager.CreateAsync(new Role()
            {
                TenantId = tenant.Id,
                Name = roleInputDto.Name,
                DisplayName = roleInputDto.DisplayName,
                IsDefault = roleInputDto.IsDefault
            });

            return ResultOutputDto.Success();
        }

        public async Task<ResultOutputDto> EditRoleAsync(RoleInput roleInputDto)
        {
            try
            {
                var output = await _roleManager.GetRoleByIdAsync(roleInputDto.Id);

                output.Name = roleInputDto.Name;
                output.DisplayName = roleInputDto.DisplayName;
                output.IsDefault = roleInputDto.IsDefault;

                var result = await _roleManager.UpdateAsync(output);

                return ResultOutputDto.FromResult(result.Succeeded);
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

        public async Task<ResultOutputDto> RemoveRoleAsync(RoleInput roleInputDto)
        {
            return await RemoveRoleByIdAsync(roleInputDto.Id);
        }

        public async Task<ResultOutputDto> RemoveRoleByIdAsync(int roleId)
        {
            try
            {
                var result = await _roleManager.DeleteAsync(await _roleManager.GetRoleByIdAsync(roleId));
                return ResultOutputDto.Success();
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

        public async Task<GetRoleOutput> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _roleManager.GetRoleByIdAsync(roleId);
                return new GetRoleOutput()
                {
                    Role = role.MapTo<RoleDto>()
                };
            }
            catch (Exception)
            {
                return new GetRoleOutput();
            }
        }

        public async Task<ResultOutputDto> SetRolePermissionsAsync(RolePermissionInput input)
        {
            try
            {
                if (input.Permissions == null || !input.Permissions.Any()) return ResultOutputDto.Failed;

                var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
                foreach (var item in input.Permissions)
                {
                    await _roleManager.GrantPermissionAsync(role, new Permission(item));
                }

                return ResultOutputDto.Success();
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

        public async Task<ResultOutputDto> RemovePermissionAsync(RemoveRolePermissionInput input)
        {
            try
            {
                await _roleManager.ProhibitPermissionAsync(await _roleManager.GetRoleByIdAsync(input.RoleId),
                    new Permission(input.Permission));

                return ResultOutputDto.Success();
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

        public async Task<ResultOutputDto> ClearRolePermissionAsync(int roleId)
        {
            try
            {
                await _roleManager.ResetAllPermissionsAsync(await _roleManager.GetRoleByIdAsync(roleId));

                return ResultOutputDto.Success();
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

        public async Task<RolePermissionOutputDto> GetRolePermissionsAsync(int roleId)
        {
            try
            {
                var result = await _roleManager.GetGrantedPermissionsAsync(roleId);

                var permissions = result.Select(item => new PermissionOutputDto()
                {
                    Name = item.Name,
                    Description = item.Description?.ToString(),
                    DisplayName = item.DisplayName?.ToString(),
                    IsGrantedByDefault = item.IsGrantedByDefault,
                    MultiTenancySides = item.MultiTenancySides
                }).ToList();

                return new RolePermissionOutputDto()
                {
                    RoleId = roleId,
                    Permissions = permissions
                };
            }
            catch (Exception)
            {
                return new RolePermissionOutputDto()
                {
                    RoleId = -1,
                };
            }
        }
    }
}