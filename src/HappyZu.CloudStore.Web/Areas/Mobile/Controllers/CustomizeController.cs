using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 定制
    /// </summary>
    public class CustomizeController : MobileControllerBase
    {
        // GET: Mobile/Customize
        public ActionResult Index()
        {
            return View();
        }
    }
}