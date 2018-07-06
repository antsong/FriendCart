using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using log4net;

namespace AntWork.Controllers
{
    [CustomActionFilter]
    public class BaseController: Controller
    {
        protected readonly OracleDbContext _DbContext;
        protected readonly ILog _Log;

        public BaseController()
        {
            _DbContext = new OracleDbContext();
            _Log = LogManager.GetLogger(typeof (BaseController));
        }

    }
}