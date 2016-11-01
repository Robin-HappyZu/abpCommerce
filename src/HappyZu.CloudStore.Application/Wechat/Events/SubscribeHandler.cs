using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Extensions;
using Abp.Runtime.Caching;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using Senparc.Weixin.MP.CommonAPIs;

namespace HappyZu.CloudStore.Wechat.Events
{
    public class SubscribeHandler : IEventHandler<SubscribeEventData>, ITransientDependency
    {
        private string appId = WebConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private string appSecret = WebConfigurationManager.AppSettings["ExternalAuth.Wechat.AppSecret"];
        private string domain = WebConfigurationManager.AppSettings["Domain"];

        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;
        public static object LockObj = new object();
        

        public SubscribeHandler(IUserAppService userAppService, ICacheManager cacheManager)
        {
            _userAppService = userAppService;
            _cacheManager = cacheManager;
        }

        public async void HandleEvent(SubscribeEventData eventData)
        {
            UserDto user = null;//await _userAppService.GetUserByWechatOpenIdAndUnionIdAsync(eventData.OpenId, string.Empty);

            if (user == null)
            {
                var cache = _cacheManager.GetCache("Wechat");

                string appToken = string.Empty;
                // 获取用户唯一凭据
                var tokenCache =await cache.GetOrDefaultAsync("ExternalAuth.Wechat.AppAccessToken");
                if (tokenCache == null)
                {
                    lock (LockObj)
                    {
                        tokenCache = cache.GetOrDefault("ExternalAuth.Wechat.AppAccessToken");
                        if (tokenCache == null)
                        {
                            var tokenResult = CommonApi.GetToken(appId, appSecret);
                            cache.Set("ExternalAuth.Wechat.AppAccessToken", tokenResult.access_token,
                                    TimeSpan.FromSeconds(tokenResult.expires_in-100));
                            appToken = tokenResult.access_token;
                        }
                        else
                        {
                            appToken = tokenCache.ToString();
                        }
                    }
                }
                else
                {
                    appToken = tokenCache.ToString();
                }
                // 获取用户信息
                var userInfo2 =await CommonApi.GetUserInfoAsync(appToken, eventData.OpenId);

                var nickName = string.Empty;
                if (!string.IsNullOrEmpty(userInfo2.nickname))
                {
                    nickName = userInfo2.nickname;
                }
                var input = new CreateUserInput()
                {
                    UserName = nickName,
                    EmailAddress = nickName + "@" + domain,
                    IsActive = true,
                    Name = nickName,
                    Surname = nickName,
                    Password = Guid.NewGuid().ToString("N").Truncate(16),
                    UnionID = userInfo2.unionid,
                    WechatOpenID = eventData.OpenId,
                    IsSubscribe = userInfo2.subscribe != 0
                };
                // 创建新用户
                await _userAppService.CreateUserAsync(input);
            }
            else
            {
                await _userAppService.SetSubscribe(user.Id);
            }

        }
    }
}
