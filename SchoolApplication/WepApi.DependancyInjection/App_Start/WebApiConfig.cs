using DependancyInjection;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;
using System.Web.Http;
using System.Collections.Generic;
using WebApiContrib.IoC.Ninject;

namespace WepApi.DependancyInjection
{
    public static class WebApiConfig
    {
        public static StandardKernel Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
            var kernel = new StandardKernel();
            Register(config, kernel);
            return kernel;
        }
        public static void Register(HttpConfiguration config, IKernel kernal)
        {
            //var modules = new List<INinjectModule>
            //{
            //    new NInjectBinding()
            //};

            // kernal.Load(modules);
            //  config.DependencyResolver = new NinjectResolver(kernal);
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

    }
}
