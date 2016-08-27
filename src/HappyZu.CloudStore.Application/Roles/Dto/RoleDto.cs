using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsDefault { get; set; }

        public bool IsStatic { get; set; }

        public User CreatorUser { get; set; }

        public User LastModifierUser { get; set; }
    }
}
