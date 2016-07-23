using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Owin;

namespace GenericBackend
{
    public partial class Startup
    {
        public void ConfigurateAutofac(HttpConfiguration configuration, IAppBuilder app)
        {
            var container = GetAutofacContainer();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(configuration);
        }

        private IContainer GetAutofacContainer()
        {
            var diFilePath = HostingEnvironment.MapPath("~/App_Data/dependencies.xml");

            if (!File.Exists(diFilePath))
            {
                throw new FileNotFoundException("autofac dependencies");
            }

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var configReader = new ConfigurationSettingsReader("autofac-dependencies", diFilePath);
            builder.RegisterModule(configReader);

            var container = builder.Build();

            return container;
        }
    }
}