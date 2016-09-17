using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 旅游
    /// </summary>
    /// <returns></returns>
    public class ToursController : MobileControllerBase
    {
        // GET: Mobile/Tours
        public ActionResult Index()
        {
            return View();
        }
    }
}