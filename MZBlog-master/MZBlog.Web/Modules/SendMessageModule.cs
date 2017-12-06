using MZBlog.Core;
using MZBlog.Core.Cache;
using MZBlog.Core.Commands.Accounts;
using MZBlog.Core.Commands.Posts;
using MZBlog.Core.Documents;
using MZBlog.Web.Security;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MZBlog.Web.Modules
{
    public class SendMessageModule : BaseNancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        private readonly ICache _cache;

        public SendMessageModule(IViewProjectionFactory viewFactory, ICache cache, ICommandInvokerFactory commandInvoker)
            : base(commandInvoker)
        {
            _cache = cache;

            _commandInvoker = commandInvoker;

            Post["/message"] = p => MessageMac(p);

            Get["/send-message"] = p =>
            {
                ViewBag.Title = "联通短信接口测试";

                return View["MessagePage"];
            };

            Post["/send-message"] = _ => SendMessage(this.BindAndValidate<MessageCommand>());

        }

        private dynamic SendMessage(MessageCommand messageCommand)
        {
            if (!ModelValidationResult.IsValid)
            {
                foreach (var err in ModelValidationResult.Errors)
                {

                }
                return View["MessagePage", new[] { "请正确输入Email和密码" }];
            }

            var userId = _cache.Get<string>("user_id");
            var token = _cache.Get<string>("access_token");

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                var loginString = Message.ReturnLoginToken("http://api.cloudmas.cn/v/1.0/login", "whdtjtadmin", "cT9QcnBk");
                var json = JsonConvert.DeserializeObject(loginString) as JObject;
                var response = json.Value<JObject>("ovit_mas_ecuser_login_response");
                userId = response.Value<string>("user_id");
                token = response.Value<string>("access_token");
                _cache.Add("user_id", userId);
                _cache.Add("access_token", token);
            }
            string resultMsg = string.Empty;
            try
            {
                var mobiles = messageCommand.Phone.Trim().Replace("\n", string.Empty).Replace("\r", string.Empty).Split(',');
                if (messageCommand.Temple)
                {
                    //测试短信内容
                    resultMsg = Message.SendSms("http://api.cloudmas.cn/v/1.1/sendSms", userId, token, mobiles, messageCommand.Content);

                }
                else
                {
                    //模板格式：['NO0003319','宝贝','超级珍贵大宝贝']
                    resultMsg = Message.SendTemplateSms("http://api.cloudmas.cn/v/1.1/sendTemplateSms", userId, token, mobiles, messageCommand.Content);
                }

                #region 写日志

                var log = new Log
                {
                    Content = string.Format("{0}在{1}发送短信返回结果{2}",
                    DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"),
                    userId,
                    resultMsg)
                };

                var command = new NewLogCommand
                {
                    Id = log.Id,
                    Content = log.Content
                };
                _commandInvoker.Handle<NewLogCommand, CommandResult>(command);

                #endregion

            }
            catch (Exception ex)
            {
                return View["MessagePage", new[] { ex.Message }];
            }
            return View["MessagePage", new[] { "短信发送成功:" + resultMsg }];
        }

        public dynamic MessageMac(dynamic p)
        {
            var dic = new Dictionary<string, string>();
            var keys = ((Nancy.DynamicDictionary)Request.Form).Keys;
            foreach (var key in keys)
            {
                dic.Add(key, Request.Form[key]);
            }
            return VerifyMac.CallMac(dic, Message.token);
        }

    }
}