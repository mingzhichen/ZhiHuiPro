using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JieYiGuang.Web.Startup))]
namespace JieYiGuang.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
