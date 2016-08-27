using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using HappyZu.CloudStore.Authorization;

namespace HappyZu.CloudStore
{
    [DependsOn(typeof(CloudStoreCoreModule), typeof(AbpAutoMapperModule))]
    public class CloudStoreApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Authorization.Providers.Add<CloudStoreAppAuthorizationProvider>();
        }
    }
}
