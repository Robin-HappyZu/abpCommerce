using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Abp.Authorization;
using Abp.UI;
using Abp.Web.Models;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Roles;
using HappyZu.CloudStore.Roles.Dto;
using HappyZu.CloudStore.Web.Areas.Admin.Models;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class SystemUserRoleController : AdminControllerBase
    {
        private readonly IRoleAppService _roleService;
        private readonly IPermissionManager _permissionManager;

        public SystemUserRoleController(IRoleAppService roleService,
            IPermissionManager permissionManager)
        {
            _roleService = roleService;
            _permissionManager = permissionManager;
        }

        // GET: Admin/SystemUserRole
        public ActionResult Index()
        {
            return View();
        }

        #region 获取用户组列表
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetRoles(GetRolesViewModel option)
        {
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
                var roles = await _roleService.GetRolesAsync();

                vm.recordsFiltered = vm.recordsTotal = roles.Items.Count;

                vm.data = roles.Items.Select(x => new RoleViewModel()
                {
                    CreateUser = x.CreatorUser != null ? x.CreatorUser.Name : string.Empty,
                    LastModifyUser = x.LastModifierUser != null ? x.LastModifierUser.Name : string.Empty,
                    IsDefault = x.IsDefault,
                    IsStatic = x.IsStatic,
                    DisplayName = x.DisplayName,
                    Name = x.Name,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {
                vm.customActionMessage = ex.Message;
                vm.customActionStatus = "";
            }

            //var order = option.order.First();
            //var orderby = order["dir"];

            #region 示例数据
            //var data=new List<object>();

            //for (int i = 0; i < 10; i++)
            //{
            //    data.Add(new RoleViewModel()
            //    {
            //        CreateUser = "CreateUser",
            //        LastModifyUser = "Last Modify User",
            //        IsDefault = false,
            //        IsStatic = false,
            //        DisplayName = "RoleName"+i,
            //        Name = "Role"+i,
            //        Id = i
            //    });
            //}

            //vm.data = data;
            #endregion

            if (vm.data == null)
            {
                vm.data = new List<object>();

                //vm.data.
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 设置用户组

        public async Task<ActionResult> SetUserRole(long id)
        {
            var roles = await _roleService.GetRolesAsync();
            var roleList = roles.Items.Select(x => new RoleViewModel()
            {
                CreateUser = x.CreatorUser != null ? x.CreatorUser.Name : string.Empty,
                LastModifyUser = x.LastModifierUser != null ? x.LastModifierUser.Name : string.Empty,
                IsDefault = x.IsDefault,
                IsStatic = x.IsStatic,
                DisplayName = x.DisplayName,
                Name = x.Name,
                Id = x.Id
            }).ToList();
            var vm = new SetUserRoleViewModel
            {
                UserId = id,
                Roles = roleList
            };
            return PartialView(vm);
        }
        #endregion

        #region 添加用户组

        public ActionResult RoleAdd(RoleViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.ReturnUrl))
            {
                vm.ReturnUrl = Url.Action("Index", "SystemUserRole", new { area = "Admin" }, true);
            }
            return View(vm);
        }

        [HttpPost, ActionName("RoleAdd")]
        public async Task<JsonResult> RoleAddPost(RoleViewModel role)
        {
            var roleInput = new RoleInput()
            {
                DisplayName = role.DisplayName,
                Name = role.Name,
                IsDefault = role.IsDefault,
                TenantName = Tenant.DefaultTenantName
            };
            var result = await _roleService.CreateRoleAsync(roleInput);

            if (result.Code != 0)
            {
                return Json(new AjaxResponse()
                {
                    Error = new ErrorInfo(result.Code, result.Message),
                    Success = false
                });
            }

            return Json(new AjaxResponse()
            {
                Success = true,
                TargetUrl = role.ReturnUrl
            });
        }
        #endregion

        #region 编辑用户组
        public async Task<ActionResult> RoleEdit(int id, string returnUrl)
        {
            var output = await _roleService.GetRoleByIdAsync(id);

            if (output.Role == null)
            {
                throw new UserFriendlyException("哎呀!程序出错了!", "你访问的用户组不存在");
            }

            var vm = new RoleViewModel()
            {
                Id = output.Role.Id,
                Name = output.Role.Name,
                DisplayName = output.Role.DisplayName,
                IsDefault = output.Role.IsDefault,
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost, ActionName("RoleEdit")]
        public async Task<JsonResult> RoleEditPost(RoleViewModel role)
        {
            var roleInput = new RoleInput()
            {
                Id = role.Id,
                DisplayName = role.DisplayName,
                Name = role.Name,
                IsDefault = role.IsDefault,
                TenantName = Tenant.DefaultTenantName
            };

            var result = await _roleService.EditRoleAsync(roleInput);

            if (result.Code != 0)
            {
                return Json(new AjaxResponse()
                {
                    Error = new ErrorInfo(result.Code, result.Message),
                    Success = false
                });
            }

            return Json(new AjaxResponse()
            {
                Success = true,
                TargetUrl = role.ReturnUrl
            });
        }
        #endregion  

        public async Task<ActionResult> PermissionSetting(PermissionViewModel vm)
        {

            var roles = await _roleService.GetRolesAsync();
            vm.Roles = roles.Items.Select(x => new RoleViewModel()
            {
                CreateUser = x.CreatorUser != null ? x.CreatorUser.Name : string.Empty,
                LastModifyUser = x.LastModifierUser != null ? x.LastModifierUser.Name : string.Empty,
                IsDefault = x.IsDefault,
                IsStatic = x.IsStatic,
                DisplayName = x.DisplayName,
                Name = x.Name,
                Id = x.Id
            }).ToList();

            vm.Permissions = _permissionManager.GetAllPermissions();

            return View(vm);
        }

        public async Task<JsonResult> GetRolePermissions(int id)
        {
            var output = await _roleService.GetRolePermissionsAsync(id);

            return Json(output.Permissions);

        }

        [HttpPost]
        public async Task<JsonResult> SetRolePermissions(SetRolePermissionsViewModel vm)
        {
            var output = await _roleService.SetRolePermissionsAsync(new RolePermissionInput()
            {
                RoleId = vm.RoleId,
                Permissions = vm.PermissionNames
            });
            if (!output.Status)
            {
                return Json(new AjaxResponse(false));
            }
            return Json(new { code = 0 });
        }

        public async Task<JsonResult> RemoveRolePermission(int id, string permissionName)
        {
            var output = await _roleService.RemovePermissionAsync(new RemoveRolePermissionInput()
            {
                RoleId = id,
                Permission = permissionName
            });
            if (!output.Status)
            {
                return Json(new AjaxResponse(false));
            }
            return Json(new { code = 0 });
        }
    }
}