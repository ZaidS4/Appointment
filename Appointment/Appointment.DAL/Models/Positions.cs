using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.DAL.Models
{
    [Table("Positions")]
    public class Positions
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public int? ModifyOn { get; set; }

        public int? CreatedBy { get; set; }


        public virtual ICollection<Reminders> Reminders { get; set; }

    }
}
