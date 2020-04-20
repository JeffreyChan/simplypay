using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using NSwag.AspNet.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace SimplePayment.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            //Remove the XM Formatter from the web api
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "simplepay.api",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterModule(new EfModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseCors(CorsOptions.AllowAll);

            // OWIN WEB API SETUP:
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseWebApi(config);
        }
    }
}