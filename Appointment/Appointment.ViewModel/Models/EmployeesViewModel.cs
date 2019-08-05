using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class EmployeesViewModel
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public bool IsActive { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<EmployeesGroupsViewModel> EmployeesGroups { get; set; }
        public virtual ICollection<RemindersViewModel> Reminders { get; set; }

    }
}
