using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace HappyZu.CloudStore
{
    [DependsOn(typeof(CloudStoreCoreModule), typeof(AbpAutoMapperModule))]
    public class CloudStoreApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
