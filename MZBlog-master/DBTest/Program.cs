using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBTest
{
    class Program
    {
        public static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        static void Main(string[] args)
        {
            TestSocket();
            Console.ReadKey();
        }

        public static void TestSocket()
        {
            FleckLog.Level = LogLevel.Debug;

            var server = new WebSocketServer("ws://192.168.0.106:8000");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    allSockets.ForEach(x => x.Send(x.ConnectionInfo + message));
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
            });

            var timer = new System.Timers.Timer
            {
                Enabled = true,
                Interval = 100
            };
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var socket in allSockets)
            {
                socket.Send(DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
            }
        }

        public static void Test()
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
