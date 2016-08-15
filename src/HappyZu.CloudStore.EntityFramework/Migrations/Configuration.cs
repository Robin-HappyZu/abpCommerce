using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using HappyZu.CloudStore.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace HappyZu.CloudStore.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CloudStore.EntityFramework.CloudStoreDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CloudStore";
        }

        protected override void Seed(CloudStore.EntityFramework.CloudStoreDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
