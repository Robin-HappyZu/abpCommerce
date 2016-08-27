using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.Authorization.Roles
{
    public class RoleManager : AbpRoleManager<Role, User>
    {
        private readonly IRepository<Role> _roleRepository;
        public RoleManager(
            IRepository<Role> roleRepository,
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetAllListAsync()
        {
            return await _roleRepository.GetAllListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _roleRepository.CountAsync();
        } 
    }
}