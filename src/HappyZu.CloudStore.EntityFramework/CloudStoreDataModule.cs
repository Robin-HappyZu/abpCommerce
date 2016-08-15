using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using HappyZu.CloudStore.EntityFramework;

namespace HappyZu.CloudStore
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(CloudStoreCoreModule))]
    public class CloudStoreDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CloudStoreDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
