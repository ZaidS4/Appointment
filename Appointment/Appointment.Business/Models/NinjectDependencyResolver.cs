using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;


namespace Appointment.Business.Models
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        //AddBindings method to register the interface in kernel
        private void AddBindings()
        {

            kernel.Bind<IAppointmentRepository>().To<AppointmentRepository>();
        }

    }
}
