using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MZBlog.Core;
using Nancy;

namespace MZBlog.Web.Modules
{
    public class SenchaTouchModule : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        public SenchaTouchModule(ICommandInvokerFactory commandInvoker)
        {
            _commandInvoker = commandInvoker;

            Get["/sencha"] = p =>
            {
                ViewBag.Title = "Sencha Touch 首页";

                return Response.AsFile("Web/sencha/index.html", "text/html");
            };

            Get["/extjs4"] = p =>
            {
                ViewBag.Title = "Extjs 4 首页";

                return Response.AsFile("Web/extjs4/index.html", "text/html");
            };
        }
    }
}