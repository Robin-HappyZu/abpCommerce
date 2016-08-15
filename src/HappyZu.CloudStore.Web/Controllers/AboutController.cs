using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Controllers
{
    public class AboutController : CloudStoreControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}