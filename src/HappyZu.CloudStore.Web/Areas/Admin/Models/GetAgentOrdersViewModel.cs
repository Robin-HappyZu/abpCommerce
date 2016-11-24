using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyZu.CloudStore.Agents;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class GetAgentOrdersViewModel : DataTableOptionViewModel
    {
        public RebateStatus RebateStatus { get; set; }

        public string AgentName { get; set; }

        public string UserName { get; set; }
    }
}