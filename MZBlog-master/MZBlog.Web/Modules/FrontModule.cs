using MZBlog.Core;
using MZBlog.Core.ViewProjections.Home;
using MZBlog.Web.Features;
using Nancy;

namespace MZBlog.Web.Modules
{
    public class FrontModule : BaseNancyModule
    {
        protected IViewProjectionFactory _viewFactory;

        public FrontModule(IViewProjectionFactory viewFactory, ICommandInvokerFactory _commandInvoker)
            :base(_commandInvoker)
        {
            _viewFactory = viewFactory;

            Before += SetAuthDisplayName;

            After.AddItemToEndOfPipeline(SetRecentBlogPosts);
            After.AddItemToEndOfPipeline(SetTagCloud);
            After.AddItemToEndOfPipeline(NancyCompressionExtenstion.CheckForCompression);
        }

        private void SetTagCloud(NancyContext obj)
        {
            ViewBag.TagCould =
                _viewFactory.Get<TagCloudBindingModel, TagCloudViewModel>(new TagCloudBindingModel() { Threshold = 2 });
        }

        private void SetRecentBlogPosts(NancyContext obj)
        {
            ViewBag.Recent =
                _viewFactory.Get<RecentBlogPostSummaryBindingModel, RecentBlogPostSummaryViewModel>(
                    new RecentBlogPostSummaryBindingModel { Page = 10 });
        }


        private Response SetAuthDisplayName(NancyContext obj)
        {
            ViewBag.CurrentUser = _viewFactory.Get<string, MZBlog.Core.Documents.Author>(MZBlog.Web.Security.FormsAuthentication.GetAuthUsernameFromCookie(obj));
            return null;
        }
    }
}