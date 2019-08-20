using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class Permissions
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public int CreatedBy { get; set; }

        public IEnumerable<object> All()
        {
            throw new NotImplementedException();
        }
    }
}
