using System.Web.Http;
using WebActivatorEx;
using OwinWebApi;
using Swashbuckle.Application;
using System.Reflection;
using System.IO;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace OwinWebApi
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "OwinApi");
                    c.PrettyPrint();
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                })
                .EnableSwaggerUi(c =>{});
        }
    }
}