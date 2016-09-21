using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComprasWebApp.Startup))]
namespace ComprasWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
