using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class TravelController : AdminControllerBase
    {
        // GET: Admin/Travel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditTravel()
        {
            return View();
        }

        public ActionResult Travelers()
        {
            return View();
        }

        //public ActionResult TravelerDetail()
        //{
        //    return View();
        //}

        public ActionResult TravelOrders()
        {
            return View();
        }
        
    }
}