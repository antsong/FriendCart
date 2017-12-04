using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DBTest
{
    public class VerifyMac
    {
        public static string CallMac(Dictionary<string, string> dictionary, string token)
        {
            var keys = dictionary.Select(x => x.Key).ToArray();
            Array.Sort(keys);
            var sb = new StringBuilder();

            foreach (var key in keys)
            {
                var value = dictionary[key];
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    sb.Append(key).Append(value);
                }
            }
            sb.Append(token);
            Console.WriteLine(sb);
            return MD5Encrypt64(sb.ToString());
        }

        public static string MD5Encrypt64(string vaule)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(vaule);
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

            return sb.ToString().ToUpper();
        }
    }
}