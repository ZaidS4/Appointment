using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class SettingsViewModel
    {
        public int ID { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public int CreatedBy { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

    }
}
