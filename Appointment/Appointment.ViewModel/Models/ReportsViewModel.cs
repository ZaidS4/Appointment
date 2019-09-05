using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class ReportsViewModel
    {
        public int TypeID { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]


        public DateTime StartDate { get; set; }


        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        [Display(Name = "Year")]
        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^([0-9]){4}$", ErrorMessage = "please enter year.")]
        [MaxLength(4, ErrorMessage = "Year cant be more than 4 numbers.")]
        [MinLength(4, ErrorMessage = "Year cant be less than 4 numbers.")]
        public string year { get; set; }



        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required.")]
        public int SelectedType { get; set; }




     
    }
}
