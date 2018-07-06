using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace AntWork
{
    public class CustomActionFilter : ActionFilterAttribute,IExceptionFilter
    {
        private readonly ILog LoggerHelper;
        public CustomActionFilter()
        {
            LoggerHelper = LogManager.GetLogger(typeof(CustomActionFilter));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            LoggerHelper.Info(filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            LoggerHelper.Info(filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            LoggerHelper.Info(filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            LoggerHelper.Info(filterContext.RouteData);
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            var route = filterContext.RouteData.Values;
            string ErrorMsg = $"在访问{route["controller"]}/{route["action"]} 时发生异常";
            LoggerHelper.Error(ErrorMsg, filterContext.Exception);
        }
    }
}