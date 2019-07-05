using Autofac;
using Autofac.Integration.WebApi;
using CodeBase.Core.Interfaces;
using CodeBase.Core.Services;
using CodeBase.Infrastructure.Data;
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Infrastructure.Commons.Configuration>().As<Core.Commons.IConfiguration>();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Specification"))
               .AsImplementedInterfaces().InstancePerRequest();

            Container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}