using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class GetUserListViewModel : DataTableOptionViewModel
    {
        public string UserName { get; set; }

        public string NickName { get; set; }
    }
}