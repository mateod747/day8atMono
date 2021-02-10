using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Service.Common;
using Service;
using Repository;
using Repository.Common;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using day2.Controller;
using Model;

namespace Container
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<PepperModelModule>();

            //var config = new HttpConfiguration();   -- iz nekog razloga s Http konfiguracijom ne radi?
            builder.RegisterType<PepperController>().InstancePerRequest();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}
