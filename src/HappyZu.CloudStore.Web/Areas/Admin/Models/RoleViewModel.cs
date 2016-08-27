using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string CreateUser { get; set; }

        public string LastModifyUser { get; set; }

        public bool IsDefault { get; set; }

        public bool IsStatic { get; set; }

        public string ReturnUrl { get; set; }
    }
}