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
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(30)]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int? PositionID { get; set; }

        public string Position { get; set; }


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
