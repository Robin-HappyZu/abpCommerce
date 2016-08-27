using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MultiTenancy;

namespace HappyZu.CloudStore.Roles.Dto
{
    public class PermissionOutputDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool IsGrantedByDefault { get; set; }

        public MultiTenancySides MultiTenancySides { get; set; }
    }
}
