using Autofac;
using Library.DataAcessLayer.Repository;
using Library.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class AutoFacModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Service
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .InstancePerLifetimeScope();
            #endregion

            #region repository
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            //builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .InstancePerLifetimeScope();
            #endregion
            base.Load(builder);
        }
    }
}