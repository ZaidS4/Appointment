using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class RemindersGroups
    {
        public int ID { get; set; }

        public int ReminderID { get; set; }

        public int GroupID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public virtual Reminders Reminders { get; set; }

        public virtual Groups Groups { get; set; }

    }
}
