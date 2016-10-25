using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 定制
    /// </summary>
    public class CustomizeController : MobileControllerBase
    {
        private readonly ICustomizeTripAppService _customizeTripAppService;

        public CustomizeController(ICustomizeTripAppService customizeTripAppService)
        {
            _customizeTripAppService = customizeTripAppService;
        }

        #region 私人定制
        // GET: Mobile/Customize
        public ActionResult Index()
        {
            ViewBag.Title = "定制·包团";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "historyback",
                        Icon = "icon icon-left",
                        Url = "javascript:window.history.back();"
                    }
                }
            };
            return View();
        }

        [HttpPost, AbpMvcAuthorize]
        public async Task<JsonResult> CreateCustomize(CustomizeTripDto input)
        {
            if (input==null)
            {
                throw new UserFriendlyException(500, "参数错误");
            }
            if (string.IsNullOrWhiteSpace(input.Destination) ||
                string.IsNullOrWhiteSpace(input.Depart) ||
                input.Days<=0 ||
                string.IsNullOrWhiteSpace(input.Contact) ||
                string.IsNullOrWhiteSpace(input.Mobile))
            {
                throw new UserFriendlyException(500, "参数错误");
            }
            input.CustomerId = AbpSession.GetUserId();
            var result = await _customizeTripAppService.SubmitCustomizationAsync(input);
            return Json(result);
        }

        #endregion

        #region 我的定制
        [AbpMvcAuthorize]
        public ActionResult MyCustomize()
        {
            ViewBag.Title = "我的定制";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "historyback",
                        Icon = "icon icon-left",
                        Url = Url.Action("Index","Account", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> GetMyCustomizes(int start,int length)
        {
            var userId = AbpSession.GetUserId();
            var input=new QueryCustomizationsInput()
            {
                CustomerId = userId,
                SkipCount = start,
                MaxResultCount = length
            };
            var result = await _customizeTripAppService.QueryCustomizationsAsync(input);
            return Json(result);
        }
        #endregion
    }
}