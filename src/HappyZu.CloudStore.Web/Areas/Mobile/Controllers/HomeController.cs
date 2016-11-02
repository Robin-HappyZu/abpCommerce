using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Abp.Extensions;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using HappyZu.CloudStore.Wechat.Events;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class HomeController : MobileControllerBase
    {
        private readonly IDestAppService _destAppService;
        private readonly IUserAppService _userAppService;

        public HomeController(IDestAppService destAppService, IUserAppService userAppService)
        {
            _destAppService = destAppService;
            _userAppService = userAppService;
        }

        // GET: Mobile/Home
        public ActionResult Index()
        {
            //var openid = "oJBwJwX5yEpaOAAGb1z6fvlc42oQ";


            //await EventBus.TriggerAsync(new SubscribeEventData
            //{
            //    OpenId = openid
            //});
            

            ViewBag.Title = "首页";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = false,
                Title = ViewBag.Title,
                ShowSearchBar = true,
                LeftButtonItems = new []
                {
                    new BarButtonItem()
                    {
                        Name = "Location",
                        DisplayName = "长沙",
                        Icon = "icon icon-location",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Account",
                        //DisplayName = "个人中心",
                        Icon = "icon icon-me",
                        Url = Url.Action("Index","Account", new {area="Mobile",type="empty"},true)
                    }
                }
            };
            return View();
        }

        public async Task<JsonResult> GetElites(int start,int length)
        {
            var city = Request.Cookies.Get("city");
            var cityid = 0;
            if (city!=null)
            {
                int.TryParse(city.Value, out cityid);
            }
            if (cityid==0)
            {
                var cityDto=await _destAppService.GetDefaultCity();
                if (cityDto!=null)
                {
                    Response.Cookies.Add(new HttpCookie("city", cityDto.Id.ToString()));
                    cityid = cityDto.Id;
                }
            }
            var input = new GetDestsInput()
            {
                SkipCount=start,
                MaxResultCount=length,
                CityId= cityid
            };

            var result=await _destAppService.GetDestsByLocationAsync(input);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}