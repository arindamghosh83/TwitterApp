using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MyTwitterApp.DependencyResolver
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // controllers
            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(Controller))
                .LifestyleTransient());

            // interfaces & classes
            container.Register(Classes.FromThisAssembly()
                .Pick()
                .WithServiceDefaultInterfaces()
                .LifestyleTransient());
        }
    }
}