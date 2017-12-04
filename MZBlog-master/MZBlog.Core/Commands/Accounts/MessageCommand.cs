using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.Commands.Accounts
{
    public class MessageCommand
    {
        [Required(ErrorMessage = "电话是必须的")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "短信内容是必须的")]
        public string Content { get; set; }

        public bool Temple { get; set; }
    }
}
