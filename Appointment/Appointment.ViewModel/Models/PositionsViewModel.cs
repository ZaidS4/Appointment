using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class PositionsViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public int? ModifyOn { get; set; }

        public int? CreatedBy { get; set; }
    }
}
