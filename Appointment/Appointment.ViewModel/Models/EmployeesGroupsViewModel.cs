using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Appointment.ViewModel.Models
{
    public class EmployeesGroupsViewModel
    {
        public int ID { get; set; }
        public int? EmployeeID { get; set; }
        public int GroupID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyOn { get; set; }
        public Nullable<int> CreatedBY { get; set; }

        //[StringLength(30)]
        //[Required]
        public string Name { get; set; }
        
        public virtual EmployeesViewModel Employee { get; set; }
        public virtual GroupsViewModel Groups { get; set; }
        public List<SelectListItem> Employees { get; set; }
        public List<EmployeesViewModel> Employeelist { get; set; }
        public  int[] SelectedEmployeesID { get; set; }
        //public List <EmployeesGrupsViewModel> EmployeesGroupViewModel { get; set; }


    }
}
