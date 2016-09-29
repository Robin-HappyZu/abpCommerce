using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Runtime.Caching;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class WechatController : Controller
    {
        private readonly string appId = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private readonly string secret = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppSecret"];

        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public WechatController(IUserAppService userAppService, ICacheManager cacheManager)
        {
            _userAppService = userAppService;
            _cacheManager = cacheManager;
        }

        [WechatAuthFilter]
        public ActionResult Index()
        {
            return Content("hello world!");
        }

        /// <summary>
        /// 微信后台回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> AuthCallback(string code, string state)
        {
            // OAuth2 授权    
            OAuthAccessTokenResult result = null;

            //通过，用code换取OpenId
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            var openId = result.openid;
            var unionId = result.unionid;

            var cache = _cacheManager.GetCache("Wechat");

            string appToken = string.Empty;
            // 获取用户唯一凭据
            var tokenCache = await cache.GetOrDefaultAsync("ExternalAuth.Wechat.AppAccessToken" );
            if (tokenCache == null)
            {
                var tokenResult = await CommonApi.GetTokenAsync(appId, secret);
                await
                    cache.SetAsync("ExternalAuth.Wechat.AppAccessToken", tokenResult.access_token,
                        TimeSpan.FromSeconds(tokenResult.expires_in));
                appToken = tokenResult.access_token;
            }
            else
            {
                appToken = tokenCache.ToString();
            }

            // 获取用户信息
            var userInfo2 = CommonApi.GetUserInfo(appToken, openId);

            var nickName = string.Empty;
            if (!string.IsNullOrEmpty(userInfo2.nickname))
            {
                nickName = userInfo2.nickname;
            }

            // 
            var user = await _userAppService.GetUserByWechatOpenIdAndUnionIdAsync(openId, unionId);
            if (user != null)
            {
                // 登陆
                var userLoginInput = new UserLoginInput()
                {
                    User = null,
                    LoginProvider = "Wechat",
                    ProviderKey = $"{openId}|{unionId}"
                };

                var loginResult = await _userAppService.UserLoginAsync(userLoginInput);
                await SignInAsync(user.MapTo<User>(), loginResult.Identity, true);
            }
            else
            {
                var input = new CreateUserInput()
                {
                    UserName = nickName,
                    EmailAddress = nickName + "@wechat.com",
                    IsActive = true,
                    Name = nickName,
                    Surname = nickName,
                    Password = CreateRandomPassword(),
                    UnionID = unionId,
                    WechatOpenID = openId
                };
                // 创建新用户
                await _userAppService.CreateUserAsync(input);
                // 添加第三方登陆凭据
                user = await _userAppService.GetUserByWechatOpenIdAndUnionIdAsync(openId, unionId);

                var userLoginInput = new UserLoginInput()
                {
                    User = user,
                    LoginProvider = "Wechat",
                    ProviderKey = $"{openId}|{unionId}"
                };

                await _userAppService.AddUserLoginAsync(userLoginInput);

                // 登陆
                var loginResult = await _userAppService.UserLoginAsync(userLoginInput);
                await SignInAsync(user.MapTo<User>(), loginResult.Identity, true);
            }
            
            return Redirect(state);
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userAppService.CreateIdentityAsync(user);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }
    }
}