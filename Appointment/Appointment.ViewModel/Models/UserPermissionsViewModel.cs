using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class UserPermissionsViewModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int PermissionID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public int CreatedBy { get; set; }
    }
}
