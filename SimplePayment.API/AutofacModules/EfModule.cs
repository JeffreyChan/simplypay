using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using SimplePayment.Repository;

namespace SimplePayment.API
{
    public class EfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SimplePaymentContext()).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}