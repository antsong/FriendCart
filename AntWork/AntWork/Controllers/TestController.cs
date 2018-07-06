using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace AntWork.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            var testQuestions = _DbContext.TestQuestions;
            return View(testQuestions);
        }

        public ActionResult Create()
        {
            var model = new TestQuestion();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TestQuestion test)
        {
            test.CreatedOn = DateTime.Now;
            test.ModifiedOn = DateTime.Now;
            test.ItemTypeId = "8228C905D00C44DB89D7139986FC91F1";
            test.Id = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            _DbContext.TestQuestions.Add(test);
            _DbContext.SaveChanges();

            if (true)
            {
                return Index();
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult File()
        {
            var attach = _DbContext.HazardNotificationAttachments.
                Where(x => x.ItemTypeId == "9729319E55514C1A9D28198DFA521791").Select(x => x.Related).ToList();
            var files = _DbContext.FileDocements.
                Where(x => attach.Contains(x.Id)).OrderBy(x => x.CreatedOn).Take(100);
            _Log.Info(files);
            return View(files);
        }

        public ActionResult Code(string id)
        {
            var file = _DbContext.FileDocements.FirstOrDefault(x => x.Id == id);
            var buffer = file?.FileContent;

            //保存到临时文件夹  
            string urlPath = "fppData/Files";
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, urlPath);
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }

            string fileName = file.Name.Split('.')[0] + file.Id + file.Name.Split('.')[1];
            if (System.IO.File.Exists(Path.Combine(localPath, fileName)))
            {

            }


            return File(buffer, @"image/jpeg");
        }

        public async System.Threading.Tasks.Task<ActionResult> Question()
        {
            string url = "http://www.wh12333.gov.cn";
            WebClient wc = new WebClient();
            wc.BaseAddress = url;
            wc.Headers.Add("Accept", "text / html,application / xhtml + xml,application/ xml; q = 0.9,image / webp,image / apng,*/*;q=0.8");
            wc.Headers.Add("Accept-Encoding", "gzip, deflate");
            wc.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            wc.Headers.Add("Cache-Control", "max-age=0");
            wc.Headers.Add("Connection", "keep-alive");
            wc.Headers.Add("Cookie",
                "_gscu_227324238=28780062acdl5o61; JSESSIONID=JpL3bm5QlK6GjLwnSjQrmhJ39SvJMGgGVybh9nnVm7JfGJJK06tL!-1841388097");
            wc.Headers.Add("Host", "www.wh12333.gov.cn");
            wc.Headers.Add("Upgrade-Insecure-Requests", "1");
            wc.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");
            Stream stream = wc.OpenRead("/frontpage/mail/MailList.action");
            var read = new StreamReader(stream, Encoding.UTF8);
            ViewBag.Response = read.ReadToEnd();
            return View();
        }
    }
}