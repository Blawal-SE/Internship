using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Webpi.Test.App_Start;

namespace Webpi.Test
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public object WebAPIConfig { get; private set; }
        protected void Application_Start()
        {
            // GlobalConfiguration.Configure(WebApiConfig.Register);
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            var kernal = WebApiConfig.Register(GlobalConfiguration.Configuration);
            // GlobalConfiguration.HttpConfiguration.EnsureInitialized();
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
