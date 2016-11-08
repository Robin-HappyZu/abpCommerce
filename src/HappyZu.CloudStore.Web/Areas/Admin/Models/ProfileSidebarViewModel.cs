using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Users.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class ProfileSidebarViewModel
    {
        public UserDto User { get; set; }

        public string RoleName { get; set; }
    }
}