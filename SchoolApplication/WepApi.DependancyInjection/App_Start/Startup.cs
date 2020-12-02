using System;
using System.Threading.Tasks;
using DependancyInjection;
using IRepository;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;
using WepApi.DependancyInjection.OauthProvider;

[assembly: OwinStartup(typeof(WepApi.DependancyInjection.App_Start.Startup))]

namespace WepApi.DependancyInjection.App_Start
{
    public partial class Startup
    {   // private StudentRepository _repo = new StudentRepository();
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup()
        {
            var kernel = new StandardKernel(new NInjectBinding());
            var Iuser = kernel.Get<IUser>();
            // var user = kernel.Get<IUser>();
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Login"),
                Provider = new Provider(Iuser),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true
            };
        }
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
