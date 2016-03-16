using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog.WebApplication.Startup))]
namespace Blog.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
