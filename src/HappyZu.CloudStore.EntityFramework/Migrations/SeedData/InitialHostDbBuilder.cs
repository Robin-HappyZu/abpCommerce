using HappyZu.CloudStore.EntityFramework;
using EntityFramework.DynamicFilters;

namespace HappyZu.CloudStore.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly CloudStoreDbContext _context;

        public InitialHostDbBuilder(CloudStoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
