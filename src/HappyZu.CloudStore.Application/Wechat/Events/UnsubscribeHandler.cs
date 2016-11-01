using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.Wechat.Events
{
    public class UnsubscribeHandler : IEventHandler<UnsubscribeEventData>, ITransientDependency
    {
        private readonly IUserAppService _userAppService;

        public UnsubscribeHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public void HandleEvent(UnsubscribeEventData eventData)
        {
            _userAppService.SetUnsubscribe(eventData.OpenId);
        }
    }
}
