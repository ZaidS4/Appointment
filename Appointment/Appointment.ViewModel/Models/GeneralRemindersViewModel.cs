using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Appointment.ViewModel.Models
{
    public class GeneralRemindersViewModel
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }

       

        [Required(ErrorMessage = "StartDate is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200)]
        [Display(Name = "Breif Description")]
        public string BreifDescription { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Time is required")]
        public TimeSpan? Time { get; set; } = TimeSpan.FromTicks(DateTime.Now.Ticks);

        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime? TimeForDisplay { get { return (Time.HasValue) ? (DateTime?)DateTime.Today.Add(Time.Value) : null; } }

        [Display(Name = "Activity")]
        public bool IsActive { get; set; }


        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifyOn { get; set; }

        //public string Group { get; set; }
        public List<string> SelectedGroups { get; set; }
        //public List<GroupsViewModel> Grouplist { get; set; }

        public int? ModifyBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? TypeID { get; set; }

        public Byte[] Image { get; set; }

        public List<SelectListItem> Groups { get; set; }

        [Required(ErrorMessage ="Choose at least one group")]
        public int[] SelectedGroupsID { get; set; }

       
    }
}
