using System.Linq;
using System.Reflection;
using Autofac;
using SimplePayment.Service;

namespace SimplePayment.API
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("SimplePayment.Service"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<SimplePaymentFacade>().As<ISimplePaymentFacade>()
                .PropertiesAutowired();
        }
    }
}