using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Repository;
using Repository.Common;

namespace Repository
{
    public class RepositoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PepperRepository>().As<IPepperRepository>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
