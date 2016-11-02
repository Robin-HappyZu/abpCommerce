using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Abp.Runtime.Caching;
using Abp.Json;
using HappyZu.CloudStore.Wechat.Utilities;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace HappyZu.CloudStore.Wechat
{
    public class WechatAppService : IWechatAppService
    {
        private readonly string _appId = WebConfigurationManager.AppSettings["ExternalAuth.Wechat.AppId"];
        private readonly string _appSecret = WebConfigurationManager.AppSettings["ExternalAuth.Wechat.AppSecret"];
        private readonly ICacheManager _cacheManager;
        public static object LockObj = new object();

        public WechatAppService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var cache = _cacheManager.GetCache("Wechat");
            var appToken=string.Empty;
            // 获取用户唯一凭据
            var tokenCache = await cache.GetOrDefaultAsync("ExternalAuth.Wechat.AppAccessToken");
            if (tokenCache == null)
            {
                lock (LockObj)
                {
                    tokenCache = cache.GetOrDefault("ExternalAuth.Wechat.AppAccessToken");
                    if (tokenCache == null)
                    {
                        var filePath = Server.GetMapPath("~/App_Data/Wechat.txt");
                        AccessTokenResult tokenResult = null;
                        if (File.Exists(filePath))
                        {
                            FileInfo fileInfo = new FileInfo(filePath);
                            var lastWriteTime = fileInfo.LastWriteTime;
                            var spanTime = DateTime.Now - lastWriteTime;
                            if (spanTime < new TimeSpan(1, 50, 0))
                            {
                                var token = File.ReadAllText(filePath);
                                tokenResult = JsonSerializationHelper.DeserializeWithType<AccessTokenResult>(token);
                                if (tokenResult != null)
                                {
                                    appToken = tokenResult.access_token;

                                    cache.Set("ExternalAuth.Wechat.AppAccessToken", tokenResult.access_token,
                                            TimeSpan.FromSeconds(spanTime.TotalSeconds - 100));
                                }
                            }

                        }
                        if (tokenResult == null)
                        {
                            tokenResult = CommonApi.GetToken(_appId, _appSecret);
                            cache.Set("ExternalAuth.Wechat.AppAccessToken", tokenResult.access_token,
                                    TimeSpan.FromSeconds(tokenResult.expires_in - 100));
                            appToken = tokenResult.access_token; ;

                            File.WriteAllText(filePath, tokenResult.ToJsonString());
                        }
                      
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

            return appToken;
        }
    }
}
