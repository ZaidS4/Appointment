using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class LookupCategoryViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public int InternalID { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }


    }
}
