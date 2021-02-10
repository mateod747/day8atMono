using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Service;
using Service.Common;

namespace Service
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PepperService>().As<IPepperService>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
