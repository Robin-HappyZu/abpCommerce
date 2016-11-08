using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Runtime.Session;
using Abp.Web.Models;
using HappyZu.CloudStore.Roles;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly UserManager _userManager;

        public UserController(IUserAppService userAppService, UserManager userManager, IRoleAppService roleAppService)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            _roleAppService = roleAppService;
        }

        #region 用户资料
        /// <summary>
        /// 用户概要
        /// Robin Z.
        /// 2016-05-25
        /// </summary>
        /// <returns></returns>
        public new ActionResult Profile()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProfileSidebar()
        {
            var userId = AbpSession.GetUserId();
            var user =  _userAppService.GetUserByIdAsync(userId).Result;
            var userRole =_userManager.GetRolesAsync(userId).Result;
            var roleName = string.Empty;
            if (userRole!=null)
            {
                roleName = userRole.FirstOrDefault();
            }
            return PartialView(new ProfileSidebarViewModel{ User=user,RoleName= roleName });
        }

        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountSetting()
        {
            return View();
        }
        #endregion

        #region 用户银行卡

        public ActionResult UserBanks()
        {
            return View();
        }
        #endregion

        #region 用户收货地址
        /// <summary>
        /// 用户收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Address()
        {
            return View();
        }
        #endregion

        #region 用户上下级关系

        #endregion

        #region 用户等级

        #endregion

        #region 用户列表
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
        
        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetList(GetUserListViewModel option)
        {
            var output = await _userAppService.QueryUsers(new QueryUserInput()
            {
                MaxResultCount = option.length,
                SkipCount = option.start,
                UserName = option.UserName,
                NickName = option.NickName
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
                    Id = x.Id,
                    UserName = x.UserName,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    LastLoginTime = x.LastLoginTime
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

        #region 用户等级
        /// <summary>
        /// 用户等级
        /// </summary>
        /// <returns></returns>
        public ActionResult Grades()
        {
            return View();
        }
        #endregion

        #region 用户积分
        /// <summary>
        /// 用户积分
        /// </summary>
        /// <returns></returns>
        public ActionResult Points()
        {
            return View();
        }
        #endregion

        #region 用户营销推广
        /// <summary>
        /// 用户营销推广
        /// </summary>
        /// <returns></returns>
        public ActionResult Spreads()
        {
            return View();
        }
        #endregion

        #region 用户关系
        /// <summary>
        /// 用户关系
        /// </summary>
        /// <returns></returns>
        public ActionResult Relation()
        {
            return View();
        }
        #endregion

        #region 升级日志
        /// <summary>
        /// 升级日志
        /// </summary>
        /// <returns></returns>
        public ActionResult UpgradeLogs()
        {
            return View();
        }
        #endregion
    }
}