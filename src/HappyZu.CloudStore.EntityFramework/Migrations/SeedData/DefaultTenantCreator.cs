using System.Linq;
using HappyZu.CloudStore.EntityFramework;
using HappyZu.CloudStore.MultiTenancy;

namespace HappyZu.CloudStore.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly CloudStoreDbContext _context;

        public DefaultTenantCreator(CloudStoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
