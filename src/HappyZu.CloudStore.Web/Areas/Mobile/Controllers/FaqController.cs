using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.FAQ;
using HappyZu.CloudStore.FAQ.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class FaqController : MobileControllerBase
    {
        private readonly IFAQAppService _faqAppService;

        public FaqController(IFAQAppService faqAppService)
        {
            _faqAppService = faqAppService;
        }

        // GET: Mobile/Faq
        public async Task<ViewResult> Index()
        {
            ViewBag.Title = "帮助中心";
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

            var result = await _faqAppService.GetAllCategorysAsync();

            return View(result);
        }
        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ViewResult> List(int id)
        {
            var category = await _faqAppService.GetCategoryByIdAsync(new EntityDto(id));

            ViewBag.Title = category.Name;
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
                        Url = Url.Action("Index","Faq", new {area="Mobile"},true)
                    }
                }
            };
            
            return View(category);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetLists(int id, int start, int length)
        {
            var list = await _faqAppService.GetDetailListAsync(new GetDetailListInput()
            {
                CategoryId = id,
                SkipCount = start,
                MaxResultCount = length
            });
            
            return Json(list);
        }

        #endregion

        #region 详情

        public async Task<ViewResult> Detail(int id)
        {
            ViewBag.Title = "帮助中心";
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

            var result = await _faqAppService.GetDetailByIdAsync(new EntityDto(id));
            return View(result);
        }
        #endregion
    }
}