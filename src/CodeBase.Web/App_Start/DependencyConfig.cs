using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace CodeBase.Web
{
    public class DependencyConfig
    {
        public static IContainer Container;

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterControllers(assembly);
            builder.RegisterApiControllers(assembly);

            builder.RegisterType<Infrastructure.Commons.Configuration>().As<Core.Commons.IConfiguration>();

            RegisterAssembly(builder, assembly, "Client");
            
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Container = container;
        }

        private static void RegisterAssembly(ContainerBuilder builder, Assembly assembly, string postfix)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith(postfix))
                .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}