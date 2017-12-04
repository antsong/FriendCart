using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.Documents
{
    public abstract class Base
    {
        public string Id;

        public string Creater;

        public DateTime? CreatedOn;
        public DateTime? ModifyOn;
    }
}
