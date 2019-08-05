using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class Groups
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public int? CreatedBy { get; set; }

        public virtual ICollection<EmployeesGroups> EmployeesGroups { get; set; }

        public virtual ICollection<RemindersGroups> RemindersGroups { get; set; }

    }
}
