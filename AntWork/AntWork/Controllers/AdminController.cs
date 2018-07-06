using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net.Util;
using WebGrease.Css.Extensions;

namespace AntWork.Controllers
{
    public class AdminController : BaseController
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

        public ActionResult ItemType()
        {
            _Log.Info(this);
            var itemTypes = _DbContext.ItemTypes.OrderBy(x => x.CreatedOn);
            _Log.Info("\r\n" + itemTypes);
            _Log.Info("\r\n" + itemTypes.Take(10).Skip(20));//分页错误写法
            _Log.Info("\r\n" + itemTypes.Skip(20).Take(10));//分页正确写法
            return View(itemTypes);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //保存到临时文件夹  
            string urlPath = "fppData/Uploads";

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, urlPath);
            if (Request.Files.Count == 0)
            {
                return Json(new { status = 0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            string filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                status = 0,
                filePath = filePathName
            });
        }

    }
}