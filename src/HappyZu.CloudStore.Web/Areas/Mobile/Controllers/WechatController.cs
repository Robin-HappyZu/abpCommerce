using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class WechatController : Controller
    {
        private readonly string appId = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private readonly string secret = ConfigurationManager.AppSettings["ExternalAuth.Wechat.AppSecret"];

        private readonly IUserAppService _userAppService;

        public WechatController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 微信后台回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<ActionResult> AuthCallback(string code, string state)
        {
            // OAuth2 授权    
            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                //return Content(ex.Message);
            }

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            OAuthUserInfo userInfo;
            try
            {
                userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
            }
            catch (ErrorJsonResultException ex)
            {
                //return Content(ex.Message);
            }

            var openId = result.openid;
            var unionId = result.unionid;
            string nickName = string.Empty;
            if (!string.IsNullOrEmpty(User?.Identity.Name))
            {
                nickName = User.Identity.Name;
            }

            // 
            var user = await _userAppService.GetUserByWechatOpenIdAndUnionIdAsync(openId, unionId);
            if (user != null)
            {
                if (user.UserName != nickName)
                {
                    user.UserName = nickName;
                    // update User
                }
            }
            else
            {
                var input = new CreateUserInput()
                {
                    
                };
                // 创建新用户
                await _userAppService.CreateUser(input);
            }

            // 用户登陆

            return Redirect(code);
        }
    }
}