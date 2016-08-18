using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Web/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}