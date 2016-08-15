using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using HappyZu.CloudStore.EntityFramework;

namespace HappyZu.CloudStore.Migrator
{
    [DependsOn(typeof(CloudStoreDataModule))]
    public class CloudStoreMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<CloudStoreDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}