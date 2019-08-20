using Appointment.Business.Interfaces;
using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.Models
{
    public class UserService : IUsers
    {
        RemindersEntities Db = new RemindersEntities();

        public List<UsersViewModel> UserAccount()
        {
            try
            {
                var u = Db.Users.Select(x => new UsersViewModel
                {
                    ID=x.ID,
                    Name=x.Name,
                    Email=x.Email,
                    UserName=x.UserName,
                    CreatedOn=x.CreatedOn,
                    CreatedBy=x.CreatedBy.Value,
                    ModifyOn=x.ModifyOn.Value,
                    ModifyBy=x.ModifyBy.Value

                }).ToList();
                return u;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<string> UserPermissions(string userName)
        {

         var user =  Db.Users.Where(x => x.UserName == userName).FirstOrDefault();
          return  user.UserPermissions.Select(x => x.Permission.Name).ToList();
        }

       
    }
}
