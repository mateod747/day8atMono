using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Model.Common;

namespace Model
{
    public class PepperModelModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PepperModel>().As<IPepperModel>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
