using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.Interfaces
{
    public interface IUsers
    {
        List<UsersViewModel> UserAccount();
        List<string> UserPermissions(string userName);

    }
}
