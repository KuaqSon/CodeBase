using Autofac;
using Autofac.Integration.WebApi;
using CodeBase.Core.Interfaces;
using CodeBase.Core.Services;
using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Data.Repositories;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace CodeBase.Api
{
    public class DependencyConfig
    {
        public static IContainer Container;

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Infrastructure.Commons.Configuration>().As<Core.Commons.IConfiguration>();
            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            // repositories in assembly CodeBase.Infrastructure
            builder.RegisterAssemblyTypes(typeof(EventRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // services in assembly CodeBase.Core
            builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Specification"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}