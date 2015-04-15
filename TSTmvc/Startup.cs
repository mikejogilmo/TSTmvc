using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TSTmvc.Startup))]
namespace TSTmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
