using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    [Table("Employees")]
    public class Employees
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public bool IsActive { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<EmployeesGroups> EmployeesGroups { get; set; }
        public virtual ICollection<Reminders> Reminders { get; set; }


    }
}
