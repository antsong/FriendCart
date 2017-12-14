using MZBlog.Core;
using MZBlog.Core.Commands;
using MZBlog.Core.Commands.Dist;
using MZBlog.Core.ViewProjections.Admin;
using MZBlog.Core.ViewProjections.Dist;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace MZBlog.Web.Modules
{
    public class DistributionModule : SecureModule
    {
        private readonly ICommandInvokerFactory _commandInvokerFactory;

        public DistributionModule(IViewProjectionFactory viewProjectionFactory, ICommandInvokerFactory commandInvokerFactory) :
            base(viewProjectionFactory, commandInvokerFactory)
        {
            _commandInvokerFactory = commandInvokerFactory;

            Get["/mz-admin/distributions/{page?1}"] = _ => ShowDistributions(_.page);

            Get["/mz-admin/distribution/new"] = _ => ShowNewDistribution();
            Post["/mz-admin/distribution/new"] = _ =>
            {
                var command = this.Bind<NewDistributionCommand>();
                return CreateNewDistribution(command);
            };
            Get["/mz-admin/distribution/edit/{id}"] = _ => ShowDistributionEdit(_.id);
            Post["/mz-admin/distribution/edit/{id}"] = _ => EditDistribution(this.Bind<EditDistributionCommand>());

            Get["/mz-admin/distribution/print/{id}"] = _ => ShowPrintDistribution(_.id);

            Post["/mz-admin/distributions/search"] = _ => SearchDistributions(this.Bind<DistributionSearchBindingModel>());
        }

        private dynamic ShowDistributions(int page)
        {
            var model =
                _viewProjectionFactory.Get<AllDistributionsBindingModel, AllDistributionsViewModel>(new AllDistributionsBindingModel()
                {
                    Page = page,
                    Take = 10
                });
            return View["Distributions", model];
        }

        private dynamic ShowNewDistribution()
        {
            return View["New", new NewDistributionCommand()];
        }

        private dynamic CreateNewDistribution(NewDistributionCommand command)
        {
            var commandResult = _commandInvokerFactory.Handle<NewDistributionCommand, CommandResult>(command);

            if (commandResult.Success)
            {
                return this.Context.GetRedirect("~/mz-admin/distributions");
            }

            AddMessage("保存信息时发生错误", "warning");

            return View["Detail", command];
        }

        private dynamic ShowDistributionEdit(string distId)
        {
            var command = _viewProjectionFactory.Get<DistributionEditBindingModel, DistributionEditViewModel>(
                new DistributionEditBindingModel
                {
                    DistId = distId
                });
            return View["Edit", command];
        }

        private dynamic EditDistribution(EditDistributionCommand command)
        {
            var commandResult = _commandInvokerFactory.Handle<EditDistributionCommand, CommandResult>(command);

            if (commandResult.Success)
            {
                AddMessage("修改成功", "success");

                return ShowDistributionEdit(command.Id);
            }

            return View["Edit", commandResult.GetErrors()];
        }

        private dynamic ShowPrintDistribution(string distId)
        {
            var command = _viewProjectionFactory.Get<DistributionEditBindingModel, DistributionEditViewModel>(
                new DistributionEditBindingModel
                {
                    DistId = distId
                });
            return View["Print", command];
        }

        private dynamic SearchDistributions(DistributionSearchBindingModel search)
        {
            var model =
                _viewProjectionFactory.Get<DistributionSearchBindingModel, AllDistributionsViewModel>(search);

            return View["Distributions", model];
        }

    }
}