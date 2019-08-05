using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class EmployeesGroups
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int GroupID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public int? CreatedBY { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Groups Groups { get; set; }

    }
}
