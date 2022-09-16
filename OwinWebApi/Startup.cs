using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using OwinWebApi;
using System.Net.Http.Headers;
using System.Web.Http;

[assembly: OwinStartup(typeof(OwinAPI.Startup))]
namespace OwinAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            var container = ContainerInit.GetContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseWebApi(config);
        }
    }
}