using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.Documents
{
    public class Log : Base
    {
        public Log()
        {
            Id = ObjectId.NewObjectId();
        }

        public string Content;
    }
}
