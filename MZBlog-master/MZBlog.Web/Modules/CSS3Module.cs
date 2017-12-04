using MZBlog.Core;
using MZBlog.Core.Commands.Posts;
using MZBlog.Core.ViewProjections.Home;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using System;
using System.Linq;

namespace MZBlog.Web.Modules
{
    public class CSS3Module : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        public CSS3Module(ICommandInvokerFactory commandInvoker)
        {
            _commandInvoker = commandInvoker;

            Get["/css3"] = p =>
            {
                ViewBag.Title = "Css3首页";

                return View["Css3Loading"];
            };
            
        }
    }
}