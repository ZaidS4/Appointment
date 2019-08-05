using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace Appointment.Business.Models
{
    public class ReminderService : IDisposable
    {
        /// <summary>
        /// gets positions from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetPositions()
        {
            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    List<EmployeeRemindersViewModel> emp = new List<EmployeeRemindersViewModel>();
                    var list = db.Positions.Select(m => new SelectListItem
                    {
                        Value = m.ID.ToString(),
                        Text = m.Name,
                    }).ToList();
                   
                    return list;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           //return model.Positions;
        }



        /// <summary>
        /// gets positions from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetEmployees()
        {
            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    List<EmployeeRemindersViewModel> emp = new List<EmployeeRemindersViewModel>();
                    var list = db.Employees.Select(m => new SelectListItem
                    {
                        Value = m.ID.ToString(),
                        Text = m.Name,
                    }).ToList();

                    return list;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            //return model.Positions;
        }


        /// <summary>
        /// gets the employee details of selected reminder by id
        /// </summary>
        /// <param name="id">selected reminder</param>
        /// <returns>selected item object</returns>
        public static EmployeeRemindersViewModel EmployeeRemindersGetByID(int id)
        {
            using (AppointmentContext db = new AppointmentContext())
            {
                Reminders reminders = db.Reminders.Where(x => x.ID == id).FirstOrDefault();


                EmployeeRemindersViewModel EmployeeReminder = new EmployeeRemindersViewModel()
                {
                    ID = reminders.ID,
                    Name = reminders.Name,
                    Email = reminders.Email,
                    BirthDate = reminders.BirthDate,
                    StartDate = reminders.StartDate,
                    PositionID = reminders.PositionID,
                    IsActive = reminders.IsActive

                };
                return EmployeeReminder;
            }

        }

        

        /// <summary>
        /// gets the general details of selected reminder by id
        /// </summary>
        /// <param name="id">selected reminder</param>
        /// <returns>selected item object</returns>
        public static GeneralRemindersViewModel generalRemindersGetByID(int id)
        {
            using (AppointmentContext db = new AppointmentContext())
            {
                Reminders reminders = db.Reminders.Where(x => x.ID == id).FirstOrDefault();


                GeneralRemindersViewModel GeneralReminder = new GeneralRemindersViewModel()
                {
                    ID = reminders.ID,
                    Name = reminders.Name,
                    StartDate = reminders.StartDate,
                    EndDate = reminders.EndDate,
                    BreifDescription = reminders.BreifDescription,
                    Time = reminders.Time,
                    CreatedOn = reminders.CreatedOn,
                    //ModifyOn = reminders.ModifyOn,
                    TypeID = reminders.TypeID,
                    IsActive = reminders.IsActive
                };
                return GeneralReminder;


            }

        }

        

        public static List<RemindersViewModel> GetAll()
        {
            List<RemindersViewModel> reminderViews = new List<RemindersViewModel>();

            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    var reminders = db.Reminders.ToList();

                    foreach (var item in reminders)
                    {

                        reminderViews.Add(new RemindersViewModel
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Email = item.Email,
                            BirthDate = item.BirthDate,
                            PositionID = item.PositionID,
                            IsActive = item.IsActive,
                            Image = item.Image,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate.Value,
                            BreifDescription = item.BreifDescription,
                            Time = item.Time,
                            EmployeeID = item.EmployeeID,
                            CreatedOn = item.CreatedOn,
                            ModifyBy = item.ModifyBy,
                            ModifyOn = item.ModifyOn,
                            CreatedBy = item.CreatedBy,
                            TypeID = item.TypeID

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reminderViews;
        }

        

            /// <summary>
            /// calls the GetAll method to reads all the reminders
            /// </summary>
            /// <returns>getall method call</returns>
        public static List<RemindersViewModel> Read()
        {
            return GetAll();
        }

        

            /// <summary>
            /// creates a new reminder of type general
            /// </summary>
            /// <param name="reminder">new reminders data</param>
        public static void Create(GeneralRemindersViewModel reminder)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();


                Reminders entity = new Reminders();

                entity.ID = reminder.ID;
                entity.Name = reminder.Name;
                entity.IsActive = reminder.IsActive;
                entity.Image = null;// reminder.Image;
                entity.StartDate = reminder.StartDate;
                entity.EndDate = reminder.EndDate;
                entity.BreifDescription = reminder.BreifDescription;
                entity.Time = reminder.Time;
                entity.CreatedOn =reminder.CreatedOn;
                //entity.ModifyBy = 0;// reminder.ModifyBy.Value;
                //entity.ModifyOn = DateTime.MinValue;
                entity.CreatedBy = reminder.CreatedBy;
                entity.TypeID = 2;

                Entities.Reminders.Add(entity);
                Entities.SaveChanges();

                reminder.ID = entity.ID;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// creates a new reminder of type employee
        /// </summary>
        /// <param name="reminder">new reminders data</param>
        public static void Create(EmployeeRemindersViewModel reminder)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();

                var items = Entities.Reminders.Where(x => x.Email == reminder.Email).ToList();

                if (items.Count < 1)
                {
                    var entity = new Reminders();

                    entity.ID = reminder.ID;
                    entity.Name = reminder.Name;
                    entity.Email = reminder.Email;
                    entity.BirthDate = reminder.BirthDate;
                    entity.PositionID = reminder.PositionID;
                    entity.IsActive = reminder.IsActive;
                    entity.Image = null;// reminder.Image;
                    entity.StartDate = reminder.StartDate;
                    entity.EmployeeID = 4;
                    entity.ModifyBy = reminder.ModifyBy;
                    entity.CreatedBy = reminder.CreatedBy;
                    entity.TypeID = 1;

                    Entities.Reminders.Add(entity);
                    Entities.SaveChanges();

                    reminder.ID = entity.ID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// method to edit the reminder of type employee
        /// </summary>
        /// <param name="reminder">edited data of reminder</param>
        public static void EmployeeReminderUpdate(EmployeeRemindersViewModel reminder)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();


                Reminders entity = new Reminders();

                entity.Name = reminder.Name;
                entity.Email = reminder.Email;
                entity.BirthDate = Convert.ToDateTime(reminder.BirthDate);
                entity.PositionID = reminder.PositionID;
                entity.IsActive = reminder.IsActive;
                //entity.Image = reminder.Image;
                entity.StartDate = reminder.StartDate;
                //entity.EmployeeID = reminder.EmployeeID;
                entity.CreatedOn = reminder.CreatedOn;
                entity.ModifyBy = reminder.ModifyBy;
                entity.ModifyOn = reminder.ModifyOn;
                entity.CreatedBy = reminder.CreatedBy;
                entity.TypeID = reminder.TypeID;


                Entities.Reminders.Attach(entity);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.SaveChanges();






            }

            catch (Exception ex)
            {
                throw;
            }
        }

        
        /// <summary>
        /// method to edit the reminder of type general
        /// </summary>
        /// <param name="reminder">edited data of reminder</param>
        public static void GeneralReminderUpdate(GeneralRemindersViewModel reminder)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();


                Reminders entity = new Reminders();

                entity.Name = reminder.Name;
                entity.IsActive = reminder.IsActive;
                //entity.Image = reminder.Image;
                entity.StartDate = reminder.StartDate;
                entity.EndDate = reminder.EndDate;
                entity.BreifDescription = reminder.BreifDescription;
                entity.Time = reminder.Time;
                entity.CreatedOn = reminder.CreatedOn;
                entity.ModifyBy = reminder.ModifyBy;
                //entity.ModifyOn = reminder.ModifyOn;
                entity.CreatedBy = reminder.CreatedBy;
                entity.TypeID = reminder.TypeID;


                Entities.Reminders.Attach(entity);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.SaveChanges();
                                                                   
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        

            /// <summary>
            /// Deleting selected reminders
            /// </summary>
            /// <param name="reminder">selected reminder</param>
        public static void Delete(RemindersViewModel reminder)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();


                Reminders entity = new Reminders();

                entity.ID = reminder.ID;

                Entities.Reminders.Attach(entity);

                Entities.Reminders.Remove(entity);

                var reminderDetails = Entities.Reminders.Where(pd => pd.ID == entity.ID);

                foreach (var reminderDetail in reminderDetails)
                {
                    Entities.Reminders.Remove(reminderDetail);
                }

                Entities.SaveChanges();




            }
            catch (Exception ex)
            {
                throw;
            }



        }

        

        /// <summary>
        /// Dispose is for releasing "unmanaged" resources ,
        /// and if it's being called outside a finalizer, 
        /// for disposing other IDisposable objects it holds that are no longer useful.
        /// </summary>
        public void Dispose()
        {
            AppointmentContext Entities = new AppointmentContext();

            Entities.Dispose();
        }
    }
}
