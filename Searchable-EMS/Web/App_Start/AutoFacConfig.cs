using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Web
{
    public class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //Register web Api controllers
            //builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<Type>().As<IType>();
            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();
            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());
            // Register our Data dependencies
            builder.RegisterModule(new AutoFacModules());
            // builder.RegisterType<AuthService>().As<IAuthService>();
            var container = builder.Build();
            // Set MVC DI resolver to us our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //  GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}