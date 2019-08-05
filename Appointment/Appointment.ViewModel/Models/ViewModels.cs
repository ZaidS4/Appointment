using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class ViewModels
    {
        public List<EmployeesGroupsViewModel> EmployeesGroupsViewModel { get; set; }

        public List<EmployeesViewModel> EmployeesViewModel { get; set; }

        public List<GroupsViewModel> GroupsViewModel { get; set; }

        public List<LookupCategoryViewModel> LookupCategoryViewModel { get; set; }

        public List<lookupsViewModel> lookupsViewModel { get; set; }

        public List<PermissionsViewModels> PermissionsViewModels { get; set; }

        public List<PositionsViewModel> PositionsViewModel { get; set; }

        public List<RemindersGroupsViewModel> RemindersGroupsViewModel { get; set; }

        public List<RemindersViewModel> RemindersViewModel { get; set; }

        public List<SettingsViewModel> SettingsViewModel { get; set; }

        public List<UserPermissionsViewModel> UserPermissionsViewModel { get; set; }

        public List<UsersViewModel> UsersViewModel { get; set; }

    }
}
