using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class CalendarViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Reminder Date")]
        public DateTime? TheDate { get; set; }

        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }


    }
}
