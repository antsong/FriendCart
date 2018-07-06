using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core
{
    public class IisLogs
    {
        public readonly string DirecPath;

        public IisLogs(string direcpath)
        {
            this.DirecPath = direcpath;
        }

        public FileInfo[] ReadFiles()
        {
            //iis用户怎样访问C盘资源
            if (!Directory.Exists(DirecPath))
                return null;
            var directoryInfo = new DirectoryInfo(DirecPath);
            return directoryInfo.GetFiles();
        }

        public FileInfo ReadFiles(string fileName)
        {
            if (!Directory.Exists(DirecPath))
                return null;
            var directoryInfo = new DirectoryInfo(DirecPath);
            return directoryInfo.GetFiles(fileName).FirstOrDefault();
        }

        public List<LogData> GetLogDatas(FileInfo file)
        {
            List<LogData> logs = new List<LogData>();
            using (var filestream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var stream = new StreamReader(filestream);
                while (!stream.EndOfStream)
                {
                    var lienConeten = stream.ReadLine();
                    if (lienConeten != null && !lienConeten.Contains("#"))
                    {
                        logs.Add(FormatLogData(lienConeten));
                    }
                }
                stream.Close();
                filestream.Close();
            }
            return logs;
        }

        public LogData FormatLogData(string content)
        {
            var contents = content.Split(' ');
            if (contents.Length != 17)
                return new LogData();
            return new LogData
            {
                DateTime = DateTime.Parse(string.Concat(contents[0], " ", contents[1])),
                Server = contents[2],
                Method = contents[3],
                UriStem = contents[4],
                UriQuery = contents[5],
                ServerPort = contents[6],
                ClientUserName = contents[7],
                Client = contents[8],
                UserAgent = contents[9],
                Referer = contents[10],
                Status = decimal.Parse(contents[11]),
                SubStatus = decimal.Parse(contents[12]),
                Win32Status = decimal.Parse(contents[13]),
                Bytes = decimal.Parse(contents[14]),
                ClentBytes = decimal.Parse(contents[15]),
                TimeTaken = decimal.Parse(contents[16])
            };
        }

        public class LogData
        {
            public DateTime DateTime { get; set; }

            public string Server { get; set; }

            public string Method { get; set; }

            public string UriStem { get; set; }

            public string UriQuery { get; set; }

            public string ServerPort { get; set; }

            public string ClientUserName { get; set; }

            public string Client { get; set; }

            public string UserAgent { get; set; }

            public string Referer { get; set; }

            public decimal Status { get; set; }

            public decimal SubStatus { get; set; }

            public decimal Win32Status { get; set; }

            public decimal Bytes { get; set; }

            public decimal ClentBytes { get; set; }

            public decimal TimeTaken { get; set; }
        }
    }
}
