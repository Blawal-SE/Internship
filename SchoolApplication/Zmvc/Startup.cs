using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zmvc.Startup))]
namespace Zmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
