using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class GroupsViewModel
    {
            public int ID { get; set; }
            public int? ModifyBy { get; set; }
            public int? CreatedBy { get; set; }


            public DateTime CreatedOn { get; set; }
            public DateTime? ModifyOn { get; set; }
            public string Name { get; set; }
            public int EmployeesNumber { get; set; }

            public int EmployeeID { get; set; }
            //public int GroupID { get; set; }
            //public int ReminderID { get; set; }


            public List<GroupsViewModel> Group { get; set; }

            public virtual ICollection<EmployeesGroupsViewModel> EmployeesGroups { get; set; }
            public virtual ICollection<RemindersGroupsViewModel> RemindersGroups { get; set; }

     
    }
}
