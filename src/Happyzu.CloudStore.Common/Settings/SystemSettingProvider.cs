using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;

namespace Happyzu.CloudStore.Common.Settings
{
    public class SystemSettingProvider:SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                        "Domain",
                        ConfigurationManager.AppSettings["Domain"]??"happizu.com",
                        isVisibleToClients:true
                    ), 
            };
        }
    }
}
