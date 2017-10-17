using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MyTwitterApp.DependencyResolver
{
    public static class WindsorConfig
    {
        public static IWindsorContainer Register()
        {
            var container = new WindsorContainer();
            //container.Register(Classes.FromThisAssembly());
            container.Install(new WindsorInstaller());

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator(container));
            System.Web.Mvc.DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
            return container;
        }
    }
}