using Appointment.Business.Job;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            int StartHour = Convert.ToInt32(ConfigurationManager.AppSettings["SendEmailHour"].ToString()), Startmin = Convert.ToInt32(ConfigurationManager.AppSettings["SendEmailmin"].ToString());
            JobScheduler.Start(StartHour, Startmin);
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
