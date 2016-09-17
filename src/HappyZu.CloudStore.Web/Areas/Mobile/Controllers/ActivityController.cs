using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 当地游玩
    /// </summary>
    public class ActivityController : MobileControllerBase
    {
        // GET: Mobile/Activity
        public ActionResult Index()
        {
            return View();
        }
    }
}