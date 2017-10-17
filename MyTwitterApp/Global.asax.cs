using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using MyTwitterApp.DependencyResolver;

namespace MyTwitterApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IWindsorContainer Container { get; private set; }

        public override void Init()
        {
            Container = WindsorConfig.Register();
            base.Init();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
