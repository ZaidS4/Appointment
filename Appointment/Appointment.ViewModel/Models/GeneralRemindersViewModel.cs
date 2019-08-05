using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class GeneralRemindersViewModel
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

       

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "BreifDescription is required")]
        public string BreifDescription { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Time is required")]
        public TimeSpan? Time { get; set; } = TimeSpan.FromTicks(DateTime.Now.Ticks);

        public bool IsActive { get; set; }


        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }= DateTime.Now;

        //[DataType(DataType.Date)]
        //public DateTime? ModifyOn { get; set; }// = DateTime.Now;

        public int? ModifyBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? TypeID { get; set; }

        public Byte[] Image { get; set; }


        
    }
}
