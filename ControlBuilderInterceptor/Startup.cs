using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlBuilderInterceptor.Startup))]
namespace ControlBuilderInterceptor
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
