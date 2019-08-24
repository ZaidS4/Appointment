using Appointment.Business.Job;
using Appointment.ViewModel.Models;
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
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var ex = Server.GetLastError().GetBaseException();
        //}
        protected void Application_Error(object sender, EventArgs e)
        {
            ErrorObject oError = new ErrorObject();
            Exception oException = Server.GetLastError().GetBaseException();
            Exception innerException = oException.InnerException;
            HttpException httpException = oException as HttpException;
            if (httpException == null && innerException != null)
            {
                httpException = innerException as HttpException;
            }
            Response.Clear();

            oError.ErrorMessage = oException.Message;
            //oError.FriendlyMessage = "Ooops!  There was a problem!";
            oError.HttpCode = (httpException != null) ? httpException.GetHttpCode() : 0;

            Server.ClearError();

            Response.RedirectToRoute("Error", oError);
        }

        private static void RegisterServecies()
        {
            Ninject.IKernel kernel = new StandardKernel();

            System.Web.Mvc.DependencyResolver.SetResolver(new Appointment.Business.Models.NinjectDependencyResolver(kernel));
        }

    }
}
