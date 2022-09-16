using Autofac;
using Autofac.Integration.WebApi;
using OwinWebApi.Interfaces;
using OwinWebApi.Repositories;
using System.Reflection;

namespace OwinWebApi
{
    public static class ContainerInit
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<InMemoryRepository>().As<IRepository>().SingleInstance();
            builder.RegisterInstance(new LifetimeManager()).SingleInstance();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}