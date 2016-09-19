using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class DestinationController : AdminControllerBase
    {
        private readonly IDestAppService _destAppService;
        public DestinationController(IDestAppService destAppService)
        {
            _destAppService = destAppService;
        }

        // GET: Admin/Destination
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetProvinces(int countryType, DataTableOptionViewModel option)
        {
            var output = await _destAppService.GetDestProvincesAsync(new GetDestProvincesInput()
            {
                CountryType = countryType,
                MaxResultCount = option.length,
                SkipCount = option.start
            });
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
                    DestType = x.DestType,
                    IsDeleted = x.IsDeleted,
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


        public ActionResult ProvinceCreate()
        {
            return View(new AddDestProvinceInput());
        }

        [HttpPost, ActionName("ProvinceCreate")]
        public async Task<JsonResult> ProvinceCreatePost(AddDestProvinceInput input)
        {
            var output = await _destAppService.AddDestProvinceAsync(input);

            return Json(new { success = output.Status });
        }

        public async Task<ActionResult> ProvinceEdit(int id)
        {
            var output =await _destAppService.GetDestProvinceByIdAsync(id);
            var input=new UpdateDestProvinceInput()
            {
                Province = output
            };
            return View(input);
        }

        [HttpPost, ActionName("ProvinceEdit")]
        public async Task<JsonResult> ProvinceEditPost(UpdateDestProvinceInput input)
        {
            var output = await _destAppService.UpdateDestProvinceAsync(input);

            return Json(new { success = output.Status });
        }

        public ActionResult Cities()
        {
            return View();
        }
        
    }
}