using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Nancy.Hosting.Aspnet;

namespace MZBlog.Web.Features
{
    public class ValidateCode
    {
        public static MemoryStream GetValidateImg(out string code, string bgImg = "content/images/vcodebg.jpg")
        {
            code = GetValidateCode();
            var rnd = new Random();
            var img = new Bitmap((int) Math.Ceiling(code.Length*17.2), 28);
            var bg = Image.FromFile(Path.Combine(new AspNetRootPathProvider().GetRootPath(), bgImg));
            var g = Graphics.FromImage(img);
            var ms = new MemoryStream();

            try
            {
                var font = new Font("Arial", 16, FontStyle.Regular | FontStyle.Italic);
                var fontbg = new Font("Arial", 16, FontStyle.Regular | FontStyle.Italic);
                var brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue,
                    Color.DarkRed, 1.2f, true);
                g.DrawImage(bg, 0, 0,
                    new Rectangle(rnd.Next(bg.Width - img.Width), rnd.Next(bg.Height - img.Height), img.Width,
                        img.Height), GraphicsUnit.Pixel);
                g.DrawString(code, fontbg, Brushes.White, 0, 1);
                g.DrawString(code, font, Brushes.Green, 0, 1); //字颜色

                //画图片的背景噪音线 
                var x = img.Width;
                var y1 = rnd.Next(5, img.Height);
                var y2 = rnd.Next(5, img.Height);
                g.DrawLine(new Pen(Color.Green, 2), 1, y1, x - 2, y2);

                g.DrawRectangle(new Pen(Color.Transparent), 0, 10, img.Width - 1, img.Height - 1);
                img.Save(ms, ImageFormat.Jpeg);
            }
            finally
            {
                img.Dispose();
                bg.Dispose();
                g.Dispose();
            }

            return ms;
        }

        private static string GetValidateCode()
        {
            //产生五位的随机字符串
            int number;
            char code;
            var checkCode = string.Empty;
            var random = new Random();

            for (var i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number%2 == 0)
                    code = (char) ('0' + (char) (number%10));
                else if (number%3 == 0)
                    code = (char) ('a' + (char) (number%26));
                else
                    code = (char) ('A' + (char) (number%26));
                checkCode += code == '0' || code == 'O' ? "x" : code.ToString();
            }
            return checkCode;
        }
    }
}