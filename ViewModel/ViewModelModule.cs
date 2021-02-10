using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;

namespace ViewModel
{
    public class ViewModelModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PepperViewModel>().As<IViewModel>();
            base.Load(builder);
        }
    }
}
