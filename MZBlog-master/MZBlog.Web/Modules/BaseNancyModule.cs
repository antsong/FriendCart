using MZBlog.Core;
using MZBlog.Core.Commands.Posts;
using MZBlog.Core.Documents;
using MZBlog.Web.Features;
using Nancy;
using System;
using System.Collections.Generic;
using System.Dynamic;
using MZBlog.Web.Security;

namespace MZBlog.Web.Modules
{
    public class BaseNancyModule : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;
        public BaseNancyModule(ICommandInvokerFactory commandInvokerFactory)
        {
            _commandInvoker = commandInvokerFactory;
            Before += SetEmptyMessageCollection;
            Before += SetViewBagWithSettings;
            Before += BeforeExcuteWriteLog;
        }

        public dynamic Settings { get; set; }

        private Response SetViewBagWithSettings(NancyContext arg)
        {
            ViewBag.Settings = AppConfiguration.Current;
            Settings = AppConfiguration.Current;
            return null;
        }

        private Response SetEmptyMessageCollection(NancyContext arg)
        {
            ViewBag.Messages = new List<ExpandoObject>();

            return null;
        }

        private Response BeforeExcuteWriteLog(NancyContext arg)
        {
            var log = new Log
            {
                Content = string.Format("{0}：{1}调用了接口{2}", arg.Request.UserHostAddress,
                    DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"), arg.Request.Url)
            };

            var command = new NewLogCommand
            {
                Id = log.Id,
                Content = log.Content
            };

            _commandInvoker.Handle<NewLogCommand, CommandResult>(command);
            return null;
        }


        public void AddMessage(string msg, string type)
        {
            dynamic obj = new ExpandoObject();
            obj.Message = msg;
            obj.MsgType = type;
            ViewBag.Messages.Value.Add(obj);
        }

    }
}