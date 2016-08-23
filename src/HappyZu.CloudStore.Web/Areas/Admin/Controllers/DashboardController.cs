using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    [AbpAuthorize]
    public class DashboardController : AdminControllerBase
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}