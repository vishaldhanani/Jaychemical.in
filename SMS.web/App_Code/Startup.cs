using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMS.web.Startup))]
namespace SMS.web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
