﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Abp.Runtime.Caching;
using HappyZu.CloudStore.Users;
using HappyZu.CloudStore.Wechat.Utilities;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;

namespace HappyZu.CloudStore.Wechat.Handler
{
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {

        private string appId = WebConfigurationManager.AppSettings["WeixinAppId"];
        private string appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];

        public static object LockObj=new object();
        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;

        public CustomMessageHandler(IUserAppService userAppService, ICacheManager cacheManager,Stream inputStream, PostModel postModel,  int maxRecordCount = 0)
           : base(inputStream, postModel, maxRecordCount)
        {
            _userAppService = userAppService;
            _cacheManager = cacheManager;
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }
        

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
           * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
           * 只需要在这里统一发出委托请求，如：
           * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
           * return responseMessage;
           */
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "";
            return responseMessage;
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs

            #region 实例代码
            //书中例子
            //if (requestMessage.Content == "你好")
            //{
            //    var responseMessage = base.CreateResponseMessage<ResponseMessageNews>();
            //    var title = "Title";
            //    var description = "Description";
            //    var picUrl = "PicUrl";
            //    var url = "Url";
            //    responseMessage.Articles.Add(new Article()
            //    {
            //        Title = title,
            //        Description = description,
            //        PicUrl = picUrl,
            //        Url = url
            //    });
            //    return responseMessage;
            //}
            //else if (requestMessage.Content == "Senparc")
            //{
            //    //相似处理逻辑
            //}
            //else
            //{
            //    //...
            //}



            //方法一（v0.1），此方法调用太过繁琐，已过时（但仍是所有方法的核心基础），建议使用方法二到四
            //var responseMessage =
            //    ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
            //    ResponseMessageText;

            //方法二（v0.4）
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);

            //方法三（v0.4），扩展方法，需要using Senparc.Weixin.MP.Helpers;
            //var responseMessage = RequestMessage.CreateResponseMessage<ResponseMessageText>();

            //方法四（v0.6+），仅适合在HandlerMessage内部使用，本质上是对方法三的封装
            //注意：下面泛型ResponseMessageText即返回给客户端的类型，可以根据自己的需要填写ResponseMessageNews等不同类型。

            #endregion

            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();

            #region 实例代码
            //            if (requestMessage.Content == null)
            //            {

            //            }
            //            else if (requestMessage.Content == "约束")
            //            {
            //                responseMessage.Content =
            //                    @"您正在进行微信内置浏览器约束判断测试。您可以：
            //<a href=""http://sdk.weixin.senparc.com/FilterTest/"">点击这里</a>进行客户端约束测试（地址：http://sdk.weixin.senparc.com/FilterTest/），如果在微信外打开将直接返回文字。
            //或：
            //<a href=""http://sdk.weixin.senparc.com/FilterTest/Redirect"">点击这里</a>进行客户端约束测试（地址：http://sdk.weixin.senparc.com/FilterTest/Redirect），如果在微信外打开将重定向一次URL。";
            //            }
            //else if (requestMessage.Content == "托管" || requestMessage.Content == "代理")
            //{
            //    //开始用代理托管，把请求转到其他服务器上去，然后拿回结果
            //    //甚至也可以将所有请求在DefaultResponseMessage()中托管到外部。

            //    DateTime dt1 = DateTime.Now; //计时开始

            //    var responseXml = MessageAgent.RequestXml(this, agentUrl, agentToken, RequestDocument.ToString());
            //    //获取返回的XML
            //    //上面的方法也可以使用扩展方法：this.RequestResponseMessage(this,agentUrl, agentToken, RequestDocument.ToString());

            //    /* 如果有WeiweihiKey，可以直接使用下面的这个MessageAgent.RequestWeiweihiXml()方法。
            //     * WeiweihiKey专门用于对接www.weiweihi.com平台，获取方式见：http://www.weiweihi.com/ApiDocuments/Item/25#51
            //     */
            //    //var responseXml = MessageAgent.RequestWeiweihiXml(weiweihiKey, RequestDocument.ToString());//获取Weiweihi返回的XML

            //    DateTime dt2 = DateTime.Now; //计时结束

            //    //转成实体。
            //    /* 如果要写成一行，可以直接用：
            //     * responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            //     * 或
            //     *
            //     */
            //    var msg = string.Format("\r\n\r\n代理过程总耗时：{0}毫秒", (dt2 - dt1).Milliseconds);
            //    var agentResponseMessage = responseXml.CreateResponseMessage();
            //    if (agentResponseMessage is ResponseMessageText)
            //    {
            //        (agentResponseMessage as ResponseMessageText).Content += msg;
            //    }
            //    else if (agentResponseMessage is ResponseMessageNews)
            //    {
            //        (agentResponseMessage as ResponseMessageNews).Articles[0].Description += msg;
            //    }
            //    return agentResponseMessage;//可能出现多种类型，直接在这里返回
            //}
            //            else if (requestMessage.Content == "测试" || requestMessage.Content == "退出")
            //            {
            //                /*
            //                * 这是一个特殊的过程，此请求通常来自于微微嗨（http://www.weiweihi.com）的“盛派网络小助手”应用请求（http://www.weiweihi.com/User/App/Detail/1），
            //                * 用于演示微微嗨应用商店的处理过程，由于微微嗨的应用内部可以单独设置对话过期时间，所以这里通常不需要考虑对话状态，只要做最简单的响应。
            //                */
            //                if (requestMessage.Content == "测试")
            //                {
            //                    //进入APP测试
            //                    responseMessage.Content = "您已经进入【盛派网络小助手】的测试程序，请发送任意信息进行测试。发送文字【退出】退出测试对话。10分钟内无任何交互将自动退出应用对话状态。";
            //                }
            //                else
            //                {
            //                    //退出APP测试
            //                    responseMessage.Content = "您已经退出【盛派网络小助手】的测试程序。";
            //                }
            //            }
            //            else if (requestMessage.Content == "AsyncTest")
            //            {
            //                //异步并发测试（提供给单元测试使用）
            //                DateTime begin = DateTime.Now;
            //                int t1, t2, t3;
            //                System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
            //                System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
            //                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            //                DateTime end = DateTime.Now;
            //                var thread = System.Threading.Thread.CurrentThread;
            //                responseMessage.Content = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
            //                        thread.ManagedThreadId,
            //                        HttpContext.Current != null ? HttpContext.Current.ApplicationInstance.GetHashCode() : -1,
            //                        begin,
            //                        end,
            //                        t2 - t1
            //                        );
            //            }
            //            else if (requestMessage.Content == "open")
            //            {
            //                var openResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageNews>();
            //                openResponseMessage.Articles.Add(new Article()
            //                {
            //                    Title = "开放平台微信授权测试",
            //                    Description = @"点击进入Open授权页面。
            //授权之后，您的微信所收到的消息将转发到第三方（盛派网络小助手）的服务器上，并获得对应的回复。
            //测试完成后，您可以登陆公众号后台取消授权。",
            //                    Url = "http://sdk.weixin.senparc.com/OpenOAuth/JumpToMpOAuth"
            //                });
            //                return openResponseMessage;
            //            }
            //            else if (requestMessage.Content == "错误")
            //            {
            //                var errorResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            //                //因为没有设置errorResponseMessage.Content，所以这小消息将无法正确返回。
            //                return errorResponseMessage;
            //            }
            //            else if (requestMessage.Content == "容错")
            //            {
            //                Thread.Sleep(1500);//故意延时1.5秒，让微信多次发送消息过来，观察返回结果
            //                var faultTolerantResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            //                faultTolerantResponseMessage.Content = string.Format("测试容错，MsgId：{0}，Ticks：{1}", requestMessage.MsgId,
            //                    DateTime.Now.Ticks);
            //                return faultTolerantResponseMessage;
            //            }
            //            else
            //            {
            //                var result = new StringBuilder();
            //                result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);

            //                if (CurrentMessageContext.RequestMessages.Count > 1)
            //                {
            //                    result.AppendFormat("您刚才还发送了如下消息（{0}/{1}）：\r\n", CurrentMessageContext.RequestMessages.Count,
            //                        CurrentMessageContext.StorageData);
            //                    for (int i = CurrentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
            //                    {
            //                        var historyMessage = CurrentMessageContext.RequestMessages[i];
            //                        result.AppendFormat("{0} 【{1}】{2}\r\n",
            //                            historyMessage.CreateTime.ToShortTimeString(),
            //                            historyMessage.MsgType.ToString(),
            //                            (historyMessage is RequestMessageText)
            //                                ? (historyMessage as RequestMessageText).Content
            //                                : "[非文字类型]"
            //                            );
            //                    }
            //                    result.AppendLine("\r\n");
            //                }

            //                result.AppendFormat("如果您在{0}分钟内连续发送消息，记录将被自动保留（当前设置：最多记录{1}条）。过期后记录将会自动清除。\r\n",
            //                    WeixinContext.ExpireMinutes, WeixinContext.MaxRecordCount);
            //                result.AppendLine("\r\n");
            //                result.AppendLine(
            //                    "您还可以发送【位置】【图片】【语音】【视频】等类型的信息（注意是这几种类型，不是这几个文字），查看不同格式的回复。\r\nSDK官方地址：http://sdk.weixin.senparc.com");

            //                responseMessage.Content = result.ToString();
            //            }

            #endregion

            return responseMessage;
        }
        #region 实例代码

        //        /// <summary>
        //        /// 处理位置请求
        //        /// </summary>
        //        /// <param name="requestMessage"></param>
        //        /// <returns></returns>
        //        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        //        {
        //            var locationService = new LocationService();
        //            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
        //            return responseMessage;
        //        }

        //        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        //        {
        //            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
        //            responseMessage.Content = "您刚才发送的是小视频";
        //            return responseMessage;
        //        }

        //        /// <summary>
        //        /// 处理图片请求
        //        /// </summary>
        //        /// <param name="requestMessage"></param>
        //        /// <returns></returns>
        //        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        //        {
        //            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
        //            responseMessage.Articles.Add(new Article()
        //            {
        //                Title = "您刚才发送了图片信息",
        //                Description = "您发送的图片将会显示在边上",
        //                PicUrl = requestMessage.PicUrl,
        //                Url = "http://sdk.weixin.senparc.com"
        //            });
        //            responseMessage.Articles.Add(new Article()
        //            {
        //                Title = "第二条",
        //                Description = "第二条带连接的内容",
        //                PicUrl = requestMessage.PicUrl,
        //                Url = "http://sdk.weixin.senparc.com"
        //            });

        //            return responseMessage;
        //        }

        //        /// <summary>
        //        /// 处理语音请求
        //        /// </summary>
        //        /// <param name="requestMessage"></param>
        //        /// <returns></returns>
        //        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        //        {
        //            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
        //            //上传缩略图
        //            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
        //            var uploadResult = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image,
        //                                                         Server.GetMapPath("~/Images/Logo.jpg"));

        //            //设置音乐信息
        //            responseMessage.Music.Title = "天籁之音";
        //            responseMessage.Music.Description = "播放您上传的语音";
        //            responseMessage.Music.MusicUrl = "http://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
        //            responseMessage.Music.HQMusicUrl = "http://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
        //            responseMessage.Music.ThumbMediaId = uploadResult.media_id;
        //            return responseMessage;
        //        }
        //        /// <summary>
        //        /// 处理视频请求
        //        /// </summary>
        //        /// <param name="requestMessage"></param>
        //        /// <returns></returns>
        //        public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        //        {
        //            var responseMessage = CreateResponseMessage<ResponseMessageText>();
        //            responseMessage.Content = "您发送了一条视频信息，ID：" + requestMessage.MediaId;
        //            return responseMessage;
        //        }

        //        /// <summary>
        //        /// 处理链接消息请求
        //        /// </summary>
        //        /// <param name="requestMessage"></param>
        //        /// <returns></returns>
        //        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        //        {
        //            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
        //            responseMessage.Content = string.Format(@"您发送了一条连接信息：
        //Title：{0}
        //Description:{1}
        //Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
        //            return responseMessage;
        //        }
        #endregion

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            //对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
            var eventResponseMessage = base.OnEventRequest(requestMessage);
            //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }
    }
}
