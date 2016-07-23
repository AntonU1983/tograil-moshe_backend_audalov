using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(GenericBackend.Startup))]
namespace GenericBackend
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var configuration = new HttpConfiguration();
            app.UseCors(CorsOptions.AllowAll);

            WebApiConfig.Register(configuration);
            ConfigureOAuth(app);
            app.UseWebApi(configuration);
            ConfigurateAutofac(configuration, app);
        }
	}
}
