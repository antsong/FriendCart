using iBoxDB.LocalServer;
using MZBlog.Core.Documents;
using System.ComponentModel.DataAnnotations;

namespace MZBlog.Core.Commands.Accounts
{
    public class ChangePasswordCommand
    {
        public string AuthorId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "新密码和新密码确认不匹配")]
        public string NewPasswordConfirm { get; set; }
    }

    public class ChangePasswordCommandInvoker : ICommandInvoker<ChangePasswordCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public ChangePasswordCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(ChangePasswordCommand command)
        {
            var author = _db.SelectKey<Author>(DBTableNames.Authors, command.AuthorId);
            if (Hasher.GetMd5Hash(command.OldPassword) != author.HashedPassword)
            {
                return new CommandResult("旧密码不正确!");
            }

            author.HashedPassword = Hasher.GetMd5Hash(command.NewPassword);
            _db.Update(DBTableNames.Authors, author);
            return CommandResult.SuccessResult;
        }
    }

    public class ResetPasswordCommand
    {
        public string AuthorId { get; set; }
    }

    public class ResetPasswordCommandInvoker : ICommandInvoker<ResetPasswordCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public ResetPasswordCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(ResetPasswordCommand command)
        {
            var author = _db.SelectKey<Author>(DBTableNames.Authors, command.AuthorId);

            //Todo 根据邮箱、电话验证进行重置密码
            author.HashedPassword = Hasher.GetMd5Hash("888888");
            _db.Update(DBTableNames.Authors, author);
            return CommandResult.SuccessResult;
        }
    }
}