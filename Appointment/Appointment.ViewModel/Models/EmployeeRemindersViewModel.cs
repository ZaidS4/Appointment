using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace Appointment.ViewModel.Models
{
    public class EmployeeRemindersViewModel
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Name id is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "BirthDate id is required")]
        public DateTime? BirthDate { get; set; }

        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "StartDate id is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "PositionID id is required")]
        public int? PositionID { get; set; }
       
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; } 

        [DataType(DataType.Date)]
        public DateTime? ModifyOn { get; set; } 

       

        public int? ModifyBy { get; set; } 

        public int? CreatedBy { get; set; }

        public int? TypeID { get; set; }



        public Byte[] Image { get; set; }


        public int? EmployeeID { get; set; }


        public List<SelectListItem> Employees { get; set; }

        public List<SelectListItem> Positions { get; set; }
    }
}
