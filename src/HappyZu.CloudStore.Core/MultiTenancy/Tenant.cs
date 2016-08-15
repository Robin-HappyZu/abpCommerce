using Abp.MultiTenancy;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}