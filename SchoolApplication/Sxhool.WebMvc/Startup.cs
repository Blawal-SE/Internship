using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
[assembly: OwinStartup(typeof(Sxhool.WebMvc.Startup))]

namespace Sxhool.WebMvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
