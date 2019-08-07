using Appointment.Business.Job;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Appointment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            int h = 14, m = 37;
            JobScheduler.Start(h, m);
            RegisterServecies();

            //  Dependency.Register();

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError().GetBaseException();
        }
        private static void RegisterServecies()
        {
            Ninject.IKernel kernel = new StandardKernel();

            System.Web.Mvc.DependencyResolver.SetResolver(new Appointment.Business.Models.NinjectDependencyResolver(kernel));
        }
    }
}
