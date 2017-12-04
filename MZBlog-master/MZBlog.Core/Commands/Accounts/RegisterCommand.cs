using iBoxDB.LocalServer;
using MZBlog.Core.Documents;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MZBlog.Core.Commands.Accounts
{
    public class RegisterCommand
    {
        [Required(ErrorMessage = "Email是必须的")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码是必须的")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "新密码和新密码确认不匹配")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }
    }


    public class RegisterCommandResult : CommandResult
    {
        public RegisterCommandResult()
            : base()
        { }

        public RegisterCommandResult(string trrorMessage)
            : base(trrorMessage)
        { }

        public Author Author { get; set; }
    }

    public class RegisterCommandInvoker : ICommandInvoker<RegisterCommand, RegisterCommandResult>
    {
        private readonly DB.AutoBox _db;

        public RegisterCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public RegisterCommandResult Execute(RegisterCommand registerCommand)
        {
            var hashedPassword = Hasher.GetMd5Hash(registerCommand.Password);
            
            var authors = from u in _db.Select<Author>("from " + DBTableNames.Authors)
                          where u.Email == registerCommand.Email
                          select u;
            if(authors.Count() > 0)
                return new RegisterCommandResult(trrorMessage: "邮箱：" + registerCommand.Email + "已经被注册") { };

            _db.Insert(DBTableNames.Authors, new Author
            {
                Email = registerCommand.Email,
                DisplayName = registerCommand.Email.Split('@')[0],
                Roles = new[] { "admin" },
                HashedPassword = hashedPassword
            });


            authors = from u in _db.Select<Author>("from " + DBTableNames.Authors)
                      where u.Email == registerCommand.Email && u.HashedPassword == hashedPassword
                      select u;

            if (authors.Count() > 0)
                return new RegisterCommandResult() { Author = authors.FirstOrDefault() };

            return new RegisterCommandResult(trrorMessage: "注册失败") { };
                 
        }
    }
}
