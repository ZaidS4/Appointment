using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    public class lookups
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int? InternalID { get; set; }

        public int? ParentID { get; set; }

        public string Sort { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        //public virtual ICollection<Reminders> RemindersTypes { get; set; }


    }
}
