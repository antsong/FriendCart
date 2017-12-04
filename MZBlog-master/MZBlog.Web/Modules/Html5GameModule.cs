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
    public class Html5GameModule : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        public Html5GameModule(ICommandInvokerFactory commandInvoker)
        {
            _commandInvoker = commandInvoker;

            Get["/fish"] = p => { return View["FishHtml5"]; };

            Get["/card"] = p => {
                ViewBag.Url = "";
                return View["CardHtml5"]; 
            };

        }
    }
}