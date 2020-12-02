using DependancyInjection;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Webpi.Test.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            var modules = new List<INinjectModule>
               {
                new NInjectBinding()
                };
            _ninjectKernel.Load(modules);
        }

    }
}