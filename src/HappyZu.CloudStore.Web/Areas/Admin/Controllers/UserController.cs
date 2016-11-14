using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Routing;
using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using HappyZu.CloudStore.Authorization;
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
        public new async Task<ActionResult> Profile()
        {
            var userId = AbpSession.GetUserId();
            var user = await _userAppService.GetUserByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(userId);
            var roleName = string.Empty;
            if (userRole != null)
            {
                roleName = userRole.FirstOrDefault();
            }

            return View(new ProfileSidebarViewModel {User=user,RoleName=roleName});
        }


        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ProfileSetting()
        {
            var userId = AbpSession.GetUserId();
            var user = await _userAppService.GetUserByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(userId);
            var roleName = string.Empty;
            if (userRole != null)
            {
                roleName = userRole.FirstOrDefault();
            }

            return View(new ProfileSidebarViewModel { User = user, RoleName = roleName });
        }

        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize(
            PermissionNames.Administrator_UserManager,
            PermissionNames.Administrator_UserRestPassword)]
        public async Task<ActionResult> AccountSetting(long id)
        {
            var user = await _userAppService.GetUserByIdAsync(id);
            var userRole = await _userManager.GetRolesAsync(id);
            var roleName = string.Empty;
            if (userRole != null)
            {
                roleName = userRole.FirstOrDefault();
            }

            return View(new ProfileSidebarViewModel { User = user, RoleName = roleName });
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> SetUserInfo(SetUserInfoInput input)
        {
            input.UserId = AbpSession.GetUserId();
            var result= await _userAppService.SetUserInfo(input);
            return Json(new {status=result.Status});
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> SetPassword(SetPasswordInput input)
        {
            input.UserId = AbpSession.GetUserId();
            var result = await _userAppService.SetPassword(input);
            return Json(new { status = result.Status });
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserManager)]
        public async Task<JsonResult> SetAdminUserInfo(SetUserInfoInput input)
        {
            if (input.UserId <= 0)
            {
                return Json(new { status = false });
            }
            var result = await _userAppService.SetUserInfo(input);
            return Json(new { status = result.Status });
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserRestPassword)]
        public async Task<JsonResult> SetAdminPassword(SetPasswordInput input)
        {
            if (input.UserId <= 0)
            {
                return Json(new { status = false });
            }
            var result = await _userAppService.SetPassword(input);
            return Json(new { status = result.Status });
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserManager)]
        public async Task<JsonResult> SetAvatar()
        {
            return Json(null);
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserRemove)]
        public async Task<JsonResult> RemoveUser(long id)
        {
            var result = await _userAppService.RemoveUser(id);
            return Json(new { status = result.Status });
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserActive)]
        public async Task<JsonResult> ActiveUser(long id)
        {
            var result = await _userAppService.ActiveUser(id);
            return Json(new { status = result.Status });
        }

        [AbpMvcAuthorize(PermissionNames.Administrator_UserRoleManager)]
        [HttpPost]
        public async Task<JsonResult> SetUserRole(long id, string roleName)
        {
            var result = await _userAppService.SetUserRole(id, roleName);
            return Json(new { status = result.Status });
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