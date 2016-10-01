using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly ITicketAppService _ticketAppService;
        public DestinationController(IDestAppService destAppService, ITicketAppService ticketAppService)
        {
            _destAppService = destAppService;
            _ticketAppService = ticketAppService;
        }

        #region 景区管理
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
        public async Task<JsonResult> GetDests(GetDestsViewModel option)
        {
            var output = await _destAppService.GetDestsAsync(new GetDestsInput()
            {
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

                vm.recordsFiltered = vm.recordsTotal = output.TotalCount;

                vm.data = output.Items.Select(x => new
                {
                    Address = x.Address,
                    CityId = x.CityId,
                    ProvinceId = x.ProvinceId,
                    DestType = x.DestType,
                    Lng = x.Lng,
                    Lat = x.Lat,
                    IsPublished = x.IsPublished,
                    CoverImage = x.CoverImage,
                    HasTicket = x.HasTicket,
                    Title = x.Title,
                    Subject = x.Subject,
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


        public ActionResult DestCreate()
        {
            var vm=new EditDestViewModel();
            return View(vm);
        }

        [HttpPost, ActionName("DestCreate")]
        public async Task<JsonResult> DestCreatePost(EditDestViewModel vm)
        {
            var input=new AddDestInput()
            {
                Dest = vm.Dest
            };
            var output = await _destAppService.AddDestAsync(input);

            return Json(new { success = output.Status ,id=output.EntityId});
        }


        public async Task<ActionResult> DestEdit(EditDestViewModel vm)
        {
            var output = await _destAppService.GetDestByIdAsync(vm.Id);
            vm.Dest = output;
            return View(vm);
        }

        [HttpPost, ActionName("DestEdit")]//,ValidateInput(false)
        public async Task<JsonResult> DestEditPost(EditDestViewModel vm)
        {
            vm.Dest.Introduce = HttpUtility.HtmlDecode(vm.Dest.Introduce);
            vm.Dest.BookingNotice = HttpUtility.HtmlDecode(vm.Dest.BookingNotice);
            vm.Dest.Agreement = HttpUtility.HtmlDecode(vm.Dest.Agreement);
            vm.Dest.Id = vm.Id;
            var input = new UpdateDestInput()
            {
                Dest = vm.Dest
            };
            var output = await _destAppService.UpdateDestAsync(input);

            return Json(new { success = output.Status, id = output.EntityId });
        }


        /// <summary>
        /// 返回部分视图
        /// </summary>
        /// <returns></returns>
        //public ActionResult HtmlView()
        //{
        //    return PartialView();
        //}
        #endregion

        #region 目的地省份和地区
        public ActionResult Provinces()
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

        [HttpPost]
        public async Task<JsonResult> GetAllProvinces(int countryType)
        {
            var output = await _destAppService.GetAllDestProvincesAsync((CountryType)countryType);
            if (output == null || output.Items.Count<=0)
            {
                return Json(null);
            }

            var list = output.Items.Select(p => new
            {
                Name=p.Name,
                Id=p.Id
            });
            return Json(list);
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
            var output = await _destAppService.GetDestProvinceByIdAsync(id);
            var input = new UpdateDestProvinceInput()
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

        #endregion

        #region 目的地城市
        public async Task<ActionResult> Cities(int id)
        {

            var output = await _destAppService.GetDestProvinceByIdAsync(id);
            var input = new DestCityViewModel()
            {
                Province = output
            };
            return View(input);
        }

        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCities(int provinceId, DataTableOptionViewModel option)
        {
            var output = await _destAppService.GetDestCitiesByDestProvinceIdAsync(new GetDestCitiesInput()
            {
                ProvinceId = provinceId,
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
                    DestCount = x.DestCount,
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

        [HttpPost]
        public async Task<JsonResult> GetAllCities(int provinceId)
        {
            var output = await _destAppService.GetDestCitiesByDestProvinceIdAsync(new GetDestCitiesInput()
            {
                MaxResultCount = -1,
                SkipCount = 0,
                ProvinceId = provinceId
            });
            if (output == null || output.Items.Count <= 0)
            {
                return Json(null);
            }

            var list = output.Items.Select(p => new
            {
                Name = p.Name,
                Id = p.Id
            });
            return Json(list);
        }

        public async Task<ActionResult> CityCreate(int provinceId)
        {

            var output = await _destAppService.GetDestProvinceByIdAsync(provinceId);
            var vm = new EditDestCityViewModel()
            {
                Province = output,
                ProvinceId = provinceId
            };
            return View(vm);
        }

        [HttpPost, ActionName("CityCreate")]
        public async Task<JsonResult> CityCreatePost(AddDestCityInput input)
        {
            var output = await _destAppService.AddDestCityAsync(input);

            return Json(new { success = output.Status });
        }

        public async Task<ActionResult> CityEdit(int id, int provinceId)
        {

            var output = await _destAppService.GetDestProvinceByIdAsync(provinceId);
            var cityOutput = await _destAppService.GetDestCityByIdAsync(id);
            var vm = new EditDestCityViewModel()
            {
                Province = output,
                ProvinceId = provinceId,
                CityId = id,
                City = cityOutput
            };
            return View(vm);
        }


        [HttpPost, ActionName("CityEdit")]
        public async Task<JsonResult> CityEditPost(UpdateDestCityInput input)
        {
            var output = await _destAppService.UpdateDestCityAsync(input);

            return Json(new { success = output.Status });
        }

        #endregion

        #region 添加门票

        public async Task<ActionResult> TicketCreate(int destId)
        {
            var dest = await _destAppService.GetDestByIdAsync(destId);
            var vm=new EditTicketViewModel()
            {
                Dest = dest
            };
            return View(vm);
        }

        [HttpPost,ActionName("TicketCreate")]
        public async Task<JsonResult> TicketCreatePost(AddTicketInput input)
        {
            var result = await _ticketAppService.AddTicketAsync(input);

            return Json(new { success = result.Status,id=result.EntityId });
        }

        public async Task<ActionResult> TicketEdit(int id)
        {
            var ticket = await _ticketAppService.GetTicketByIdAsync(id);
            var vm=new EditTicketViewModel();
            if (ticket != null)
            {
                vm.Ticket = ticket;
                var dest = await _destAppService.GetDestByIdAsync(ticket.DestId);
                vm.Dest = dest;
            }

            return View(vm);
        }

        [HttpPost, ActionName("TicketEdit")]
        public async Task<JsonResult> TicketEditPost(UpdateTicketInput input)
        {
            var result = await _ticketAppService.UpdateTicketAsync(input);

            return Json(new { success = result.Status, id = result.EntityId });
        }
        #endregion

        #region 门票类型
        public async Task<JsonResult> GetTicketType(int destId)
        {
            var result = await _ticketAppService.GetTicketTypeListAsync(destId);
            return Json(new {list=result.Items});
        }

        [HttpPost,ActionName("TicketTypeCreate")]
        public async Task<JsonResult> TicketTypeCreatePost(AddTicketTypeInput input)
        {
            var result = await _ticketAppService.AddTicketTypeAsync(input);
            return Json(new { success = result.Status });
        }
        #endregion

    }
}