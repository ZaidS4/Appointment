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
        public string Name { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeName { get; set; }
        public DateTime BirthDate { get; set; }

        public int? EmployeeID { get; set; }

        public int GroupID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyOn { get; set; }
        public int? CreatedBY { get; set; }
        public List<SelectListItem> Employees { get; set; }

        public List<EmployeesGroupsViewModel> EmployeesGroupViewModel { get; set; }
        public virtual EmployeesViewModel Employee { get; set; }
        public virtual GroupsViewModel Groups { get; set; }

    }
}
