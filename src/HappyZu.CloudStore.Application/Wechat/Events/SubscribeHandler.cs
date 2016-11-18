using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Extensions;
using HappyZu.CloudStore.IdGenerators;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Users.Dto;
using Senparc.Weixin.MP.CommonAPIs;

namespace HappyZu.CloudStore.Wechat.Events
{
    public class SubscribeHandler : IEventHandler<SubscribeEventData>, ITransientDependency
    {
        private readonly string _domain = WebConfigurationManager.AppSettings["Domain"];

        private readonly IUserAppService _userAppService;
        private readonly IWechatAppService _wechatAppService;
        private readonly UserNameManager _userNameManager;


        public SubscribeHandler(IUserAppService userAppService, IWechatAppService wechatAppService, UserNameManager userNameManager)
        {
            _userAppService = userAppService;
            _wechatAppService = wechatAppService;
            _userNameManager = userNameManager;
        }

        public async void HandleEvent(SubscribeEventData eventData)
        {
            var user = await _userAppService.GetUserByWechatOpenIdAndUnionIdAsync(eventData.OpenId, string.Empty);

            if (user == null)
            {

                var appToken = await _wechatAppService.GetAccessTokenAsync();
                // 获取用户信息
                var userInfo2 =await CommonApi.GetUserInfoAsync(appToken, eventData.OpenId);

                var nickName = string.Empty;
                if (!string.IsNullOrEmpty(userInfo2.nickname))
                {
                    nickName = userInfo2.nickname;
                }
                var userName = _userNameManager.CreateId().ToString();
                var input = new CreateUserInput()
                {
                    UserName = userName,
                    EmailAddress = userName + "@" + _domain,
                    IsActive = true,
                    Name = nickName,
                    Surname = nickName,
                    Password = Guid.NewGuid().ToString("N").Truncate(16),
                    UnionID = userInfo2.unionid,
                    WechatOpenID = eventData.OpenId,
                    IsSubscribe = userInfo2.subscribe != 0
                };
                // 创建新用户
                await _userAppService.AddUserAsync(input);
            }
            else
            {
                await _userAppService.SetSubscribe(user.Id);
            }

        }
    }
}
