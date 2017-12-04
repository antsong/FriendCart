using MZBlog.Core;
using MZBlog.Core.Cache;
using MZBlog.Core.Commands.Accounts;
using MZBlog.Web.Security;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using ThoughtWorks.QRCode.Codec;

namespace MZBlog.Web.Modules
{
    public class AccountModule : NancyModule
    {
        private readonly ICommandInvokerFactory _commandInvoker;

        public AccountModule(ICommandInvokerFactory commandInvoker)
        {

            _commandInvoker = commandInvoker;

            Get["/mz-login"] = _ => ShowLoginPage();
            Post["/mz-login", (ctx) => ctx.Request.Form.Remember] = _ => LoginUser(this.BindAndValidate<LoginCommand>());
            Post["/mz-login", (ctx) => !ctx.Request.Form.Remember] = _ => LoginUser(this.BindAndValidate<LoginCommand>());

            Get["/mz-logout"] = _ => Logout();

            Get["/mz-register"] = _ => ShowRegisterPage();
            Post["/mz-register"] = _ => RegisterUser(this.BindAndValidate<RegisterCommand>());

            Get["/mz-code"] = _ => Code();
        }

        public Negotiator Logout()
        {
            return View["LogoutPage"].WithCookie(FormsAuthentication.CreateLogoutCookie());
        }

        public Negotiator ShowLoginPage()
        {
            ViewBag.ReturnUrl = Request.Query.returnUrl;
            return View["LoginPage"];
        }

        public dynamic LoginUser(LoginCommand loginCommand)
        {
            if (!ModelValidationResult.IsValid)
            {
                foreach (var err in ModelValidationResult.Errors)
                {

                }
                //return View["LoginPage", ModelValidationResult.Errors];
                return View["LoginPage", new[] { "请正确输入Email和密码" }];
            }

            var commandResult = _commandInvoker.Handle<LoginCommand, LoginCommandResult>(loginCommand);

            if (commandResult.Success)
            {
                var cookie = FormsAuthentication.CreateAuthCookie(commandResult.Author.Id, loginCommand.Remember);
                var response = Context.GetRedirect(loginCommand.ReturnUrl ?? "~/mz-admin");
                response.WithCookie(cookie);
                return response;
            }

            return View["LoginPage", commandResult.GetErrors()];
        }

        public Negotiator ShowRegisterPage()
        {
            ViewBag.ReturnUrl = Request.Query.returnUrl;
            return View["RegisterPage"];
        }

        public dynamic RegisterUser(RegisterCommand registerCommand)
        {
            if (registerCommand.Code != Session["code"].ToString())
                return View["RegisterPage", new[] { "验证码不正确" }];

            if (!ModelValidationResult.IsValid)
            {
                foreach (var err in ModelValidationResult.Errors)
                {

                }

                return View["RegisterPage"];
            }

            var commandResult = _commandInvoker.Handle<RegisterCommand, RegisterCommandResult>(registerCommand);

            if (commandResult.Success)
            {
                var cookie = FormsAuthentication.CreateAuthCookie(commandResult.Author.Id);
                var response = Context.GetRedirect(registerCommand.ReturnUrl ?? "~/mz-admin");
                response.WithCookie(cookie);
                return response;

            }
            return View["RegisterPage", commandResult.GetErrors()];

        }

        public dynamic Code()
        {
            string code = "";
            //生成验证码
            System.IO.MemoryStream ms = MZBlog.Web.Features.ValidateCode.GetValidateImg(out code);

            //生成二维码
            //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //qrCodeEncoder.QRCodeScale = 4;
            //qrCodeEncoder.QRCodeVersion = 8;
            //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //byte[] buffer = (byte[])new System.Drawing.ImageConverter().ConvertTo(qrCodeEncoder.Encode(new System.Random().Next(100000).ToString()), typeof(byte[]));
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);

            ms.Position = 0;

            Session["code"] = code;
            return Response.FromStream(ms, @"images/jpg");
        }

        public bool IsRegister()
        {
            string email = Request.Form["email"];
            return true;
        }
    }
}