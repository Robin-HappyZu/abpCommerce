using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.Runtime.Caching;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using HappyZu.CloudStore.Web.Areas.Mobile.Filters;
using HappyZu.CloudStore.Wechat;
using HappyZu.CloudStore.Wechat.Handler;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class WechatController : MobileControllerBase
    {
        private readonly string appId = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private readonly string secret = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppSecret"];
        private readonly string _token= ConfigurationManager.AppSettings["ExternalAuth.Wechat.ServiceToken"];
        private readonly string _encodingAesKey = ConfigurationManager.AppSettings["ExternalAuth.Wechat.EncodingAESKey"];
        private string domain = ConfigurationManager.AppSettings["Domain"];

        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;
        private readonly IWechatAppService _wechatAppService;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public WechatController(IUserAppService userAppService, ICacheManager cacheManager, IWechatAppService wechatAppService)
        {
            _userAppService = userAppService;
            _cacheManager = cacheManager;
            _wechatAppService = wechatAppService;
        }

        #region 身份认证

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

            string appToken =await _wechatAppService.GetAccessTokenAsync();

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
                    EmailAddress = nickName + "@"+ domain,
                    IsActive = true,
                    Name = nickName,
                    Surname = nickName,
                    Password = CreateRandomPassword(),
                    UnionID = unionId,
                    WechatOpenID = openId,
                    IsSubscribe = userInfo2.subscribe != 0
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
        #endregion

        [HttpGet]
        public Task<ActionResult> Service(string signature, string timestamp, string nonce, string echostr)
        {
            return Task.Factory.StartNew(() =>
            {
                if (CheckSignature.Check(signature, timestamp, nonce, _token))
                {
                    return echostr; //返回随机字符串则表示验证通过
                }
                else
                {
                    return "failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, _token) + "。" +
                        "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                }
            }).ContinueWith<ActionResult>(task => Content(task.Result));
        }
        [HttpPost,ActionName("Service")]
        public Task<ActionResult> ServicePost(PostModel postModel)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, _token))
                {
                    return new WeixinResult("参数错误！");
                }

                postModel.Token = _token;
                postModel.EncodingAESKey = _encodingAesKey; //根据自己后台的设置保持一致
                postModel.AppId = appId; //根据自己后台的设置保持一致

                var messageHandler = new CustomMessageHandler(EventBus,Request.InputStream, postModel, 10);

                messageHandler.Execute(); //执行微信处理过程

                return new FixWeixinBugWeixinResult(messageHandler);

            }).ContinueWith<ActionResult>(task => task.Result);
        }
    }
}