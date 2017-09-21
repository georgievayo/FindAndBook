using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindAndBook.Startup))]
namespace FindAndBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
