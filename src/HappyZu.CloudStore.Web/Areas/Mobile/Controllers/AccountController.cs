using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Abp.Extensions;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using HappyZu.CloudStore.Authorization;
using HappyZu.CloudStore.Entities;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Account;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Dest;
using HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout;
using HappyZu.CloudStore.Web.Controllers;
using HappyZu.CloudStore.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class AccountController : MobileControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        private readonly ITicketAppService _ticketAppService;


        public AccountController(LogInManager logInManager, UserManager userManager, ITicketAppService ticketAppService)
        {
            _logInManager = logInManager;
            _userManager = userManager;
            _ticketAppService = ticketAppService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Mobile/Account
        [AbpMvcAuthorize]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.GetUserId());
            ViewBag.Title = "个人中心";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                     new BarButtonItem()
                    {
                        Name = "Home",
                        Icon = "icon icon-home",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                },
                RightButtonItems = new[]
                {
                     new BarButtonItem()
                    {
                        Name = "Settings",
                        Icon = "icon icon-settings",
                        Url = Url.Action("Settings","Account", new {area="Mobile"},true)
                    }
                }
            };
            return View(user);
        }

        [AbpMvcAuthorize]
        public ActionResult Settings()
        {
            ViewBag.Title = "设置";
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

        #region 用户登陆/注销
        public ActionResult Login()
        {
            ViewBag.Title = "用户登录";
            ViewBag.HeaderBar = new HeaderViewModel()
            {
                ShowTitle = true,
                Title = ViewBag.Title,
                ShowSearchBar = false,
                LeftButtonItems = new[]
                {
                    new BarButtonItem()
                    {
                        Name = "Home",
                        Icon = "icon icon-home",
                        Url = Url.Action("Index","Home", new {area="Mobile"},true)
                    }
                }
            };
            return View();
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.TenancyName
                );

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "Your email address is not confirmed. You can not login"); //TODO: localize message
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #endregion

        #region 网络服务协议

        public ActionResult Agreement()
        {
            ViewBag.Title = "网络服务协议";
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
        #endregion

        #region 关于我们
        public ActionResult AboutUs()
        {
            ViewBag.Title = "关于我们";
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
        #endregion

        #region 投诉建议

        public ActionResult Complaints()
        {
            ViewBag.Title = "投诉建议";
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
        #endregion

        #region 推荐给好友

        public ActionResult Recommend()
        {
            ViewBag.Title = "推荐给好友";
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
        #endregion

        #region 我的门票
        [AbpMvcAuthorize]
        public ActionResult MyTickets()
        {
            ViewBag.Title = "我的门票";
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

        public async Task<JsonResult> GetMyTickets(GetMyTicketsViewModel vm)
        {
            var input = new GetPagedTicketOrdersInput()
            {
                UserId=AbpSession.GetUserId(),
                MaxResultCount=vm.Length,
                SkipCount=vm.Start
            };
            if (!string.IsNullOrWhiteSpace(vm.Status))
            {
                try
                {
                    input.OrderStatus = vm.Status.ToEnum<OrderStatus>();
                }
                catch
                {
                    // ignored
                }
            }
            var result = await _ticketAppService.GetTicketOrdersAsync(input);
            return Json(result);
        }
        #endregion

        #region 我的门票详情
        [AbpMvcAuthorize]
        public async Task<ActionResult> MyTicketDetail(int id)
        {
            ViewBag.Title = "门票详情";
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
            var userId = AbpSession.GetUserId();
            var order = await _ticketAppService.GetTicketOrderByIdAsync(id, userId);
            if (order==null)
            {
                throw new UserFriendlyException(404, "您访问的订单不存在");
            }
            var orderItems = await _ticketAppService.GetTicketOrderItemsByTicketOrderIdAsync(order.Id);

            var vm = new MyTicketDetailViewModel()
            {
                TicektOrder = order,
                OrderItems = orderItems
            };
            return View(vm);
        }
        #endregion

        #region 我的推广二维码
        [AbpMvcAuthorize(PermissionNames.Agents)]
        public ActionResult MyQrcode()
        {
            ViewBag.Title = "我的推广二维码";
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
            ViewBag.UserId = AbpSession.GetUserId();
            return View();
        }
        #endregion

        #region 代理商业绩
        [AbpMvcAuthorize(PermissionNames.Agents)]
        public ActionResult AgentOrder()
        {
            ViewBag.Title = "代理商业绩";
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

        [AbpMvcAuthorize(PermissionNames.Agents)]
        public JsonResult GetAgentOrders(int start, int length)
        {
            return Json(null);
        }
        #endregion
    }
}