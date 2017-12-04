using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var loginString = Message.ReturnLoginToken("http://api.cloudmas.cn/v/1.0/login", "whdtjtadmin", "cT9QcnBk");
            var json = JsonConvert.DeserializeObject(loginString) as JObject;
            if (json != null)
            {
                var response = json.Value<JObject>("ovit_mas_ecuser_login_response");
                var userId = response.Value<string>("user_id");
                var token = response.Value<string>("access_token");

                Console.WriteLine(Message.SendSms("http://api.cloudmas.cn/v/1.1/sendSms", userId, token));

                Console.WriteLine(Message.SendTemplateSms("http://api.cloudmas.cn/v/1.1/sendTemplateSms", userId, token));
            }
            Console.ReadKey();
        }

        public static string MD5Encrypt64(string vaule)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(vaule);
            byte[] targetData = md5.ComputeHash(fromData);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < targetData.Length; i++)
            {
                String hex = (targetData[i] & 0xFF).ToString("x");
                if (hex.Length == 1)
                {
                    sb.Append("0");
                }
                sb.Append(targetData[i].ToString("x"));
            }

            return sb.ToString();
        }

    }
}
