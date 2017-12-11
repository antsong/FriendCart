using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntWork.Startup))]
namespace AntWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
