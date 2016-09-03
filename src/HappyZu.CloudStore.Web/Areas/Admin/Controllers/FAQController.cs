using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using HappyZu.CloudStore.FAQ;
using HappyZu.CloudStore.FAQ.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class FAQController : AdminControllerBase
    {
        private readonly IFAQAppService _faqAppService;
        public FAQController(IFAQAppService faqAppService)
        {
            _faqAppService = faqAppService;
        }

        // GET: Admin/FAQ
        public ActionResult Index()
        {
            return View();
        }

        #region 获取所有帮助

        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAll(DataTableOptionViewModel option)
        {
            var input=new GetDetailListInput()
            {
                CategoryId = 1
            };
            var output = await _faqAppService.GetDetailListAsync(input);
            var vm = new DataTableJsonViewModel()
            {
                draw = option.draw,
                recordsTotal = 0,
                recordsFiltered = 0,
                customActionMessage = "",
                customActionStatus = "OK"
            };

            try
            {

                vm.recordsFiltered = vm.recordsTotal = output.Items.Count;

                vm.data = output.Items.Select(x => new
                {
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    IsDeleted = x.IsDeleted,
                    Sort = x.Sort,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {
                vm.customActionMessage = ex.Message;
                vm.customActionStatus = "";
            }

            if (vm.data == null)
            {
                vm.data = new List<object>();
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Category()
        {
            return View();
        }
        #region 获取分类列表
        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAllCategories(DataTableOptionViewModel option)
        {
            var output =await _faqAppService.GetAllCategorysAsync();
            var vm = new DataTableJsonViewModel()
            {
                draw = option.draw,
                recordsTotal = 0,
                recordsFiltered = 0,
                customActionMessage = "",
                customActionStatus = "OK"
            };

            try
            {

                vm.recordsFiltered = vm.recordsTotal = output.Items.Count;

                vm.data = output.Items.Select(x => new 
                {
                    Icon = x.Icon,
                    FontIcon = x.FontIcon,
                    IsEnable = x.IsEnable,
                    IsDeleted = x.IsDeleted,
                    Sort = x.Sort,
                    DetailsCount=x.DetailsCount,
                    Name = x.Name,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {
                vm.customActionMessage = ex.Message;
                vm.customActionStatus = "";
            }

            if (vm.data == null)
            {
                vm.data = new List<object>();
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 添加帮助分类
        public ActionResult CreateCategory(CreateFAQCategoryInput input)
        {
            return View(input);
        }

        [HttpPost,ActionName("CreateCategory")]
        public async Task<JsonResult> CreateCategoryPost(CreateFAQCategoryInput input)
        {
            var output =await _faqAppService.CreateCategoryAsync(input);

            return Json(new { success = output.Status });
        }

        #endregion

        #region 编辑帮助分类

        #endregion

        #region 添加帮助

        public ActionResult CreateDetail(CreateDetailInput input)
        {
            return View(input);
        }

        [HttpPost,ActionName("CreateDetail")]
        public async Task<JsonResult> CreateDetailPost(CreateDetailInput input)
        {
            var output = await _faqAppService.CreateAsync(input);
            return Json(new { success = output.Status });
        }
        #endregion

        #region 编辑帮助

        public ActionResult EditDetail(CreateDetailInput input)
        {
            return View(input);
        }

        [HttpPost, ActionName("EditDetail")]
        public JsonResult EditDetailPost()
        {
            return Json(new {});
        }
        #endregion
    }
}