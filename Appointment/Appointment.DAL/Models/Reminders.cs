using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class Reminders
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? PositionID { get; set; }

        public bool IsActive { get; set; }

        public Byte[] Image { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string BreifDescription { get; set; }

        public TimeSpan? Time { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? TypeID { get; set; }

        public virtual ICollection<Positions> Positions { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }

        public lookups type { get; set; }

    }
}
