using Appointment.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Appointment.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base

        protected override  void OnAuthentication(AuthenticationContext filterContext)
        {

            
            UserService userService = new UserService();

            //Split is an Extension method of String class
            //It seperates the comma separated roles.
            //The data comes from the controller
      

            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                var permetions = userService.UserPermissions(HttpContext.User.Identity.Name);
                if(permetions.Count==0)
                  throw new UnauthorizedAccessException();
            }
        }
    }
}