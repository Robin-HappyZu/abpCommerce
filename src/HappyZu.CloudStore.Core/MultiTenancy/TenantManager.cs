using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.Editions;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}