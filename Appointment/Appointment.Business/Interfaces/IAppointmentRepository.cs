using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.Models
{
    public interface IAppointmentRepository
    {
        //void Create(ReminderViewModel reminder);
        //void Update(ReminderViewModel reminder);
        //void Delete(ReminderViewModel reminder);
        //List<ReminderViewModel> Read();
        List<EmployeesViewModel> Read();
        List<EmployeesGroupsViewModel> ReadEmployeeGroup();
    }
}
