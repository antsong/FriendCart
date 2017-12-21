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

            Get["/search-message"] = _ => SearchMessage(_.batch);

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
            var tuple = this.LoginMessage();
            var userId = tuple.Item1;
            var token = tuple.Item2;
            string resultMsg;
            try
            {
                var mobiles = messageCommand.Phone.Trim().Replace("\n", string.Empty).Replace("\r", string.Empty).Split(',');
                resultMsg = messageCommand.Temple ?
                    Message.SendSms("http://api.cloudmas.cn/v/1.1/sendSms", userId, token, mobiles, messageCommand.Content) :
                    Message.SendTemplateSms("http://api.cloudmas.cn/v/1.1/sendTemplateSms", userId, token, mobiles, messageCommand.Content);

                #region 写日志

                var log = new Log
                {
                    Content = string.Format("【{0}】发送短信成功返回结果：{1}",
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
            return View["MessagePage", new[] {  resultMsg }];
        }

        private dynamic SearchMessage(string batch)
        {
            var tuple = this.LoginMessage();
            var userId = tuple.Item1;
            var token = tuple.Item2;
            var resultMsg = Message.SearchSms(" http://api.cloudmas.cn/v/2.1/querySmsSendStatus", userId, token);
            #region 写日志

            var log = new Log
            {
                Content = string.Format("【{0}】在查询短信，成功返回结果：{1}",
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

            return View["MessagePage", new[] { resultMsg }];
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Tuple<string, string> LoginMessage()
        {
            if (!string.IsNullOrEmpty(_cache.Get<string>("user_id")) &&
                !string.IsNullOrEmpty(_cache.Get<string>("access_token")))
            {
                return new Tuple<string, string>(_cache.Get<string>("user_id"), _cache.Get<string>("access_token"));
            }

            var loginString = Message.ReturnLoginToken("http://api.cloudmas.cn/v/1.0/login", "whdtjtadmin", "cT9QcnBk");
            var json = JsonConvert.DeserializeObject(loginString) as JObject;
            if (json == null)
            {
                throw new Exception("LoginMessage Return is null");
            }
            var response = json.Value<JObject>("ovit_mas_ecuser_login_response");
            var userId = response.Value<string>("user_id");
            var token = response.Value<string>("access_token");
            _cache.Add("user_id", userId);
            _cache.Add("access_token", token);
            return new Tuple<string, string>(userId, token);
        }
    }
}