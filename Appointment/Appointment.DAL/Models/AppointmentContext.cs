using System.Data.Entity;

namespace Appointment.DAL.Models
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext()
            : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public DbSet<Employees> Employees { get; set; }

        public DbSet<EmployeesGroups> EmployeesGroups { get; set; }

        public DbSet<Groups> Groups { get; set; }

        public DbSet<LookupCategories> LookupCategories { get; set; }

        public DbSet<lookups> Lookups { get; set; }

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<Positions> Positions { get; set; }

        public DbSet<Reminders> Reminders { get; set; }

        public DbSet<RemindersGroups> RemindersGroups { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<UserPermissions> UserPermissions { get; set; }

        public DbSet<Users> Users { get; set; }

        //public System.Data.Entity.DbSet<Appointment.ViewModel.Models.EmployeesGroupsViewModel> EmployeesGroupsViewModels { get; set; }

        //public System.Data.Entity.DbSet<Appointment.ViewModel.Models.EmployeesGroupsViewModel> EmployeesGroupsViewModels { get; set; }

    }
}
