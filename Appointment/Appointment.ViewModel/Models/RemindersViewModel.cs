using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.ViewModel.Models;

namespace Appointment.ViewModel.Models
{
    public class RemindersViewModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "BirthDate id is required")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "PositionID id is required")]
        public int? PositionID { get; set; }

        public bool? IsActive { get; set; }

        public Byte[] Image { get; set; }

        
        [Required(ErrorMessage = "StartDate id is required")]
        public DateTime? StartDate { get; set; }

        
        [Required(ErrorMessage = "EndDate id is required")]
        public DateTime? EndDate { get; set; } 

        [Required(ErrorMessage = "BreifDescription is required")]
        public string BreifDescription { get; set; }

        
        [Required(ErrorMessage = "Time is required")]
        public TimeSpan? Time { get; set; }

        public int? EmployeeID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public int? ModifyBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifyOn { get; set; } = DateTime.Now;

        public int? CreatedBy { get; set; }

        public int? TypeID { get; set; }

        public virtual PositionsViewModel Positions { get; set; }


    }
}
