using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBoxDB.LocalServer;
using MZBlog.Core.Commands.Posts;
using MZBlog.Core.Documents;

namespace MZBlog.Core.Commands.Accounts
{
    public class DeleteAuthorCommand
    {
        public string AuthorId { get; set; }
    }

    public class DeleteAuthorCommandInvoker : ICommandInvoker<DeleteAuthorCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public DeleteAuthorCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(DeleteAuthorCommand command)
        {
            _db.Delete(DBTableNames.Authors, command.AuthorId);
            return CommandResult.SuccessResult;
        }
    }
}
