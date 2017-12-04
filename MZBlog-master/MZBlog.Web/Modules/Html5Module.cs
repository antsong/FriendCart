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
    public class Html5Module : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        public Html5Module(ICommandInvokerFactory commandInvoker)
        {
            _commandInvoker = commandInvoker;

            Get["/html5"] = p =>
            {
                ViewBag.Title = "Html5首页";

                return View["Html5"];
            };

            Get["/html5animore"] = p =>
            {
                ViewBag.Title = "Html5Animore首页";

                return View["Html5Animore"];
            };

            Get["/html5-svg-shanche-animation"] = p =>
            {
                ViewBag.Title = "Html5-svg-shanche-animation首页";

                return View["html5svgshancheanimation"];
            };

            Get["/ball-pool"] = p =>
            {
                ViewBag.Title = "BallPool首页";

                return View["BallPool"];
            };

            Get["/webgl-clouds"] = p =>
            {
                ViewBag.Title = "WebglClouds首页";

                return View["WebglClouds"];
            };

            Get["/love"] = p =>
            {
                ViewBag.Title = "LOVE";

                return View["Html5Love"];
            };

        }
    }
}