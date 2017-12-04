﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DBTest
{
    public class Message
    {
        //private static readonly WebClient Instance = new WebClient();

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

        public static string SendSms(string url, string userId, string token)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("user_id", userId);
            dic.Add("sign_no", "cy6MTcmP");
            dic.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dic.Add("ext", "1");
            dic.Add("mobiles", "['13871298164','13618625064']");
            dic.Add("content", "['NO0003319','建总副总','建总总经理']");
            dic.Add("type", "2");
            return Send(url, dic, token);
        }

        public static string SendTemplateSms(string url, string userId, string token)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("user_id", userId);
            dic.Add("sign_no", "cy6MTcmP");
            dic.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dic.Add("ext", "1");
            dic.Add("mobiles", "['13871298164','13618625064']");
            dic.Add("templateName", "SYS170923110139141");
            dic.Add("templateParams", "['NO0003319','宝贝','超级珍贵大宝贝']");
            dic.Add("type", "1");
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
