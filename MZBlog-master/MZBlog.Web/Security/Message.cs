using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using MZBlog.Core;
using MZBlog.Core.Commands.Posts;
using MZBlog.Core.Documents;

namespace MZBlog.Web.Security
{
    public class Message
    {
        public static ICommandInvokerFactory _commandInvoker;

        public static List<IWebSocketConnection> _sockets = new List<IWebSocketConnection>();

        public const string userId = "59";

        public const string token = "a1f0f9ad-2bc9-41c6-9311-6cf8c59f5ab8";

        /// <summary>
        ///     Get请求需要将参数拼接到url后面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public static string ReturnLoginToken(string url, string username, string passwd)
        {
            var Instance = new WebClient();
            var path = string.Format("{0}?login_code={1}&passwd={2}", url, username, passwd);
            return Encoding.UTF8.GetString(Instance.DownloadData(path));
        }

        public static string SendSms(string url, string userId, string token, string[] mobiles, string content)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("user_id", userId);
            dic.Add("sign_no", "cy6MTcmP");
            dic.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dic.Add("ext", "1");
            dic.Add("mobiles", string.Format("['{0}']", string.Join("','", mobiles)));
            dic.Add("content", content);
            dic.Add("type", "2");
            return Send(url, dic, token);
        }

        public static string SendTemplateSms(string url, string userId, string token, string[] mobiles, string content)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("user_id", userId);
            dic.Add("sign_no", "cy6MTcmP");
            dic.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dic.Add("ext", "1");
            dic.Add("mobiles", string.Format("['{0}']", string.Join("','", mobiles)));
            dic.Add("templateName", "SYS170923110139141");
            dic.Add("templateParams", content);
            dic.Add("type", "1");
            return Send(url, dic, token);
        }

        public static string SearchSms(string url, string userId, string token)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("user_id", userId);
            dic.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return Send(url, dic, token);
        }

        private static string Send(string url, Dictionary<string, string> para, string token)
        {
            var Instance = new WebClient();
            Instance.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var postData = new StringBuilder();
            foreach (var p in para)
            {
                postData.Append(string.Format("{0}={1}&", p.Key, p.Value));
            }
            var mac = VerifyMac.CallMac(para, token);

            postData.Append(string.Format("{0}={1}", "mac", mac));
            var bytes = Encoding.UTF8.GetBytes(postData.ToString());

            return Encoding.UTF8.GetString(Instance.UploadData(url, bytes));
        }
    }
}
