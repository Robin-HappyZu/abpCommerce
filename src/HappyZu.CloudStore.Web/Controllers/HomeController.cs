using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace HappyZu.CloudStore.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CloudStoreControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}