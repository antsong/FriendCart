using iBoxDB.LocalServer;
using MZBlog.Core;
using MZBlog.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.Commands.Posts
{
    public class NewLogCommand
    {
        public ObjectId Id;

        public string Content;
    }

    public class NewLogCommandInvoker : ICommandInvoker<NewLogCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public NewLogCommandInvoker(DB.AutoBox db)
        {
            this._db = db;
        }

        public CommandResult Execute(NewLogCommand command)
        {
            var log = new Log
            {
                Id = command.Id,
                Content = command.Content,
                CreatedOn = DateTime.Now
            };

            var result = this._db.Insert(DBTableNames.Logs, log);
            return CommandResult.SuccessResult;
        }
    }
}
