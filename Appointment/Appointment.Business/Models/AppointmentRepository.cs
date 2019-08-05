using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.DAL.Models;
using System.Data.Entity;
using Appointment.ViewModel.Models;

namespace Appointment.Business.Models
{
    public class AppointmentRepository : IAppointmentRepository
    {
        //        private bool UpdateDatabase = false;

        //-------------------------------------Constructor----------------------------------------//
        private static AppointmentContext _appointmentContext;

        public AppointmentRepository(AppointmentContext entities)
        {
            _appointmentContext = entities;
        }

        //------------------------------Get EmployeesViewModel Data-------------------------------//

        public List<EmployeesViewModel> GetAll()
        {
            List<EmployeesViewModel> EmployeeViews = new List<EmployeesViewModel>();

            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    var employees = db.Employees.ToList();

                    foreach (var item in employees)
                    {

                        EmployeeViews.Add(new EmployeesViewModel
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Email = item.Email,
                            BirthDate = item.BirthDate,
                            IsActive = item.IsActive,
                            CreatedOn = item.CreatedOn,
                            ModifyBy = item.ModifyBy,
                            ModifyOn = item.ModifyOn,
                            CreatedBy = item.CreatedBy

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return EmployeeViews;
        }

        public List<EmployeesViewModel> Read()
        {
            return GetAll();
        }

        //------------------------------Get EmployeesGroupsViewModel Data-------------------------------//
        public List<EmployeesGroupsViewModel> GetAllEmployeeGroup()
        {
            List<EmployeesGroupsViewModel> employeesGroupsViews = new List<EmployeesGroupsViewModel>();

            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    var employeesGroups = db.EmployeesGroups.ToList();

                    foreach (var item in employeesGroups)
                    {

                        employeesGroupsViews.Add(new EmployeesGroupsViewModel
                        {
                            ID = item.ID,
                            EmployeeID = item.EmployeeID,
                            GroupID = item.GroupID,
                            CreatedOn = item.CreatedOn,
                            ModifyBy = item.ModifyBy,
                            ModifyOn = item.ModifyOn,
                            CreatedBY = item.CreatedBY

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return employeesGroupsViews;
        }

        public List<EmployeesGroupsViewModel> ReadEmployeeGroup()
        {
            return GetAllEmployeeGroup();
        }
    }
}

//----------------------------------------------Comments Area-----------------------------------------//
//        public void Create(ReminderViewModel reminder)
//        {
//            try
//            {
//                AppointmentContext Entities = new AppointmentContext();
//                if (UpdateDatabase)
//                {
//                    var first = GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
//                    var id = (first != null) ? first.Id : 0;

//                    reminder.Id = id + 1;




//                }
//                else
//                {

//                    var entity = new Reminder();

//                    entity.Title = reminder.Title;
//                    entity.Date = reminder.Date;
//                    entity.Subject = reminder.Subject;

//                    Entities.Reminders.Add(entity);
//                    Entities.SaveChanges();

//                    reminder.Id = entity.Id;

//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public void Update(ReminderViewModel reminder)
//        {
//            try
//            {
//                AppointmentContext Entities = new AppointmentContext();

//                if (UpdateDatabase)
//                {
//                    var target = One(e => e.Id == reminder.Id);

//                    if (target != null)
//                    {

//                        target.Title = reminder.Title;
//                        target.Subject = reminder.Subject;
//                        target.Date = reminder.Date;

//                    }

//                }
//                else
//                {
//                    var entity = new Reminder();

//                    entity.Id = reminder.Id;
//                    entity.Title = reminder.Title;
//                    entity.Subject = reminder.Subject;
//                    entity.Date = reminder.Date;


//                    Entities.Reminders.Attach(entity);
//                    Entities.Entry(entity).State = EntityState.Modified;
//                    Entities.SaveChanges();
//                }





//            }

//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public void Delete(ReminderViewModel reminder)
//        {
//            try
//            {
//                AppointmentContext Entities = new AppointmentContext();

//                if (UpdateDatabase)
//                {
//                    var target = GetAll().FirstOrDefault(p => p.Id == reminder.Id);
//                    if (target != null)
//                    {
//                        GetAll().Remove(target);
//                    }
//                }
//                else
//                {
//                    var entity = new Reminder();

//                    entity.Id = reminder.Id;

//                    Entities.Reminders.Attach(entity);

//                    Entities.Reminders.Remove(entity);

//                    var orderDetails = Entities.Reminders.Where(pd => pd.Id == entity.Id);

//                    foreach (var orderDetail in orderDetails)
//                    {
//                        Entities.Reminders.Remove(orderDetail);
//                    }

//                    Entities.SaveChanges();
//                }



//            }
//            catch (Exception ex)
//            {
//                throw;
//            }



//        }

//        public ReminderViewModel One(Func<ReminderViewModel, bool> predicate)
//        {

//            try
//            {
//                AppointmentContext Entities = new AppointmentContext();

//                return GetAll().FirstOrDefault(predicate);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//    }
//}
//-------------------------------------------------------------------------------------------------------//