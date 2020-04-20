using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SimplePayment.API
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Simple Payment Gateway");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\SimplePayment.API.xml",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi(c => { });
        }
    }
}