using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntWork.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

        public ActionResult Icons()
        {
            return View();
        }

        public ActionResult FlotChart()
        {
            return View();
        }

        public ActionResult Morris()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult PanelsWells()
        {
            return View();
        }

        public ActionResult Buttons()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            return View();
        }

        public ActionResult Typography()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFile()
        {
            //Todo接收上传的文件

            return Json(new {});
        }
    }
}