using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindAndBook.Web.Startup))]
namespace FindAndBook.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
