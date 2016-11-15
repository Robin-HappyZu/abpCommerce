﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace HappyZu.CloudStore.Wechat
{
    public interface IWechatAppService : IApplicationService
    {
        Task<string> GetAccessTokenAsync();
    }
}