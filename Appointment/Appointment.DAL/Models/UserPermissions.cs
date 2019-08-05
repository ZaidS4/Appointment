using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class UserPermissions
    {
        public int ID { get; set; }

        public int UserID { get; set; }//***1

        public int PermissionID { get; set; }//***2

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual Users Users { get; set; }

        public virtual Permissions Permissions { get; set; }

    }
}
