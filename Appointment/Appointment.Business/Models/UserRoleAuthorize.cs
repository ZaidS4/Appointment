using Appointment.Business.Interfaces;
using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Appointment.Business.Models
{
    public class UserRoleAuthorize : AuthorizeAttribute
    {
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Data Repository. Getting data from database
            var repository = new Permissions();
            UserService userService = new UserService();

            //Split is an Extension method of String class
            //It seperates the comma separated roles.
            //The data comes from the controller
            var roles = Roles.Split(',');

            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                var permetions = userService.UserPermissions(HttpContext.Current.User.Identity.Name);
                foreach (var role in roles)
                  if(!permetions.Contains(role))
                     throw new UnauthorizedAccessException();
            }
            return true;
            
        }
    }
}