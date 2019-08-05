using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class RemindersGroupsViewModel
    {
        public int ID { get; set; }

        public int ReminderID { get; set; }

        public int GroupID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public virtual RemindersViewModel RemindersViewModel { get; set; }

        public virtual GroupsViewModel GroupsViewModel { get; set; }
    }
}
