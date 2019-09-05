using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Appointment.DAL.Models;
using Appointment.ViewModel.Enums;
using System.Web;
using System.IO;
using System.DirectoryServices;

namespace Appointment.Business.Models
{
    #region testCode
    //try
    //{
    //    string DomainPath = "LDAP://DC=sssprocess,DC=com";
    //    DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
    //    SearchResult sr;
    //    //changes
    //    using (searchRoot)
    //    {
    //        DirectorySearcher search = new DirectorySearcher
    //        (
    //        searchRoot,
    //        "(&(objectCategory=person)(objectClass=user))" //any user
    //         );
    //        search.PageSize = 1000;
    //        search.ServerPageTimeLimit = TimeSpan.FromSeconds(10);
    //        using (SearchResultCollection resultCollection = search.FindAll())
    //        {

    //        }
    //    }
    //}
    //catch (Exception ex)
    //{
    //}
    #endregion
    public class ReminderService : IDisposable
    {
        /// <summary>
        /// method to get the type of Reminder by id
        /// </summary>
        /// <param name="id">selected reminder id</param>
        /// <returns>int type</returns>
        public static int? GetType(int id)
        {
            RemindersEntities db = new RemindersEntities();

            Reminder reminders = db.Reminders.Where(x => x.ID == id).FirstOrDefault();
            var type = reminders.TypeID;
            return type;
        }



        /// <summary>
        /// gets Type from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetTypeName()
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
                    var list = db.Lookups.Where(x => x.Code.Equals("1") || x.Code.Equals("2")).Select(m => new SelectListItem
                    {
                        Value = m.ID.ToString(),
                        Text = m.NameEn,
                    }).ToList();

                    return list;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// gets positions from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetPositions()
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
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

        }


        /// <summary>
        /// gets Groups from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetGroups()
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
                    var list = db.Groups.Select(m => new SelectListItem
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

        }

        /// <summary>
        /// gets Email from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of Email</returns>
        public static string GetEmail(int Id)
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {

                    var emailId = db.Employees.Find(Id).ID;
                    var email = db.Employees.Find(Id).Email;
                    return email;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// gets Email from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of Email</returns>
        //public static int GetEmailId(int Id)
        //{
        //    try
        //    {
        //        using (RemindersEntities db = new RemindersEntities())
        //        {

        //            var emailId = db.Employees.Find(Id).ID;
        //            return emailId;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// gets Email from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of Email</returns>
        public static bool CheckUsedEmployee(int Id, int? ReminderID)
        {
            try
            {



                using (RemindersEntities db = new RemindersEntities())
                {

                    var used = db.Reminders.Any(x => x.EmployeeID == Id && (ReminderID.HasValue ? (x.ID != ReminderID) : true));
                    return used;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// gets positions from DB then set it in dropdownlist
        /// </summary>
        /// <returns>list of positions</returns>
        public static List<SelectListItem> GetEmployees()
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
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

        }


        /// <summary>
        /// gets the employee details of selected reminder by id
        /// </summary>
        /// <param name="id">selected reminder</param>
        /// <returns>selected item object</returns>
        public static EmployeeRemindersViewModel EmployeeRemindersGetByID(int? id)
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                Reminder reminders = db.Reminders.Where(x => x.ID == id).FirstOrDefault();

                EmployeeRemindersViewModel EmployeeReminder = new EmployeeRemindersViewModel()
                {
                    ID = reminders.ID,
                    Name = reminders.Name,
                    Email = reminders.Email,
                    BirthDate = reminders.BirthDate,
                    StartDate = reminders.StartDate,
                    ImagePath = reminders.ImagePath,
                    PositionID = reminders.PositionID,
                    CreatedOn = reminders.CreatedOn,
                    ModifyOn = reminders.ModifyOn,
                    IsActive = reminders.IsActive.Value,
                    Position = reminders.PositionID.HasValue ? reminders.Position.Name : ""

                };
                return EmployeeReminder;
            }

        }


        /// <summary>
        /// gets the Reminder details of selected reminder by id
        /// </summary>
        /// <param name="id">selected reminder</param>
        /// <returns>selected item object</returns>
        public static RemindersViewModel RemindersGetByID(int? id)
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                Reminder reminders = db.Reminders.Where(x => x.ID == id).FirstOrDefault();

                RemindersViewModel Reminder = new RemindersViewModel()
                {
                    ID = reminders.ID,
                    Name = reminders.Name,
                    Email = reminders.Email,
                    BirthDate = reminders.BirthDate,
                    StartDate = reminders.StartDate,
                    Image = reminders.Image,
                    PositionID = reminders.PositionID,
                    CreatedOn = reminders.CreatedOn,
                    ModifyOn = reminders.ModifyOn,
                    IsActive = reminders.IsActive.Value


                };
                return Reminder;
            }

        }



        /// <summary>
        /// gets the general details of selected reminder by id
        /// </summary>
        /// <param name="id">selected reminder</param>
        /// <returns>selected item object</returns>
        public static GeneralRemindersViewModel generalRemindersGetByID(int id)
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                Reminder reminders = db.Reminders.Find(id);
                //var groups = db.Groups.ToList();
                //var remGroups = db.RemindersGroups.Where(z => z.ReminderID == reminders.ID).FirstOrDefault();




                GeneralRemindersViewModel GeneralReminder = new GeneralRemindersViewModel()
                {
                    ID = reminders.ID,
                    Name = reminders.Name,
                    StartDate = reminders.StartDate,
                    EndDate = reminders.EndDate,
                    BreifDescription = reminders.BreifDescription,
                    Time = reminders.Time,
                    CreatedOn = reminders.CreatedOn,
                    ModifyOn = reminders.ModifyOn,
                    TypeID = reminders.TypeID,
                    IsActive = reminders.IsActive.Value,
                    /*Group = groupName */// reminders..HasValue ? reminders.Position.Name : ""

                };
                List<int> selected = new List<int>();

                foreach (var ReminderGroup in reminders.RemindersGroups)
                {
                    selected.Add(ReminderGroup.GroupID);
                }

                GeneralReminder.SelectedGroupsID = selected.ToArray();

                var reminderGroups = reminders.RemindersGroups;
                GeneralReminder.SelectedGroups = new List<string>();
                if (reminderGroups != null)
                {
                    foreach (var RMG in reminderGroups)
                    {
                        GeneralReminder.SelectedGroups.Add(RMG.Group == null ? "" : RMG.Group.Name);
                    }
                }
                return GeneralReminder;
            }

        }


        /// <summary>
        /// method that reads all reminder from DB
        /// </summary>
        /// <returns>list of remindersViewModel</returns>
        public static List<RemindersViewModel> GetAll()
        {
            List<RemindersViewModel> reminderViews = new List<RemindersViewModel>();

            try
            {

                using (RemindersEntities db = new RemindersEntities())
                {
                    var reminders = db.Reminders.ToList();
                    var remTypes = db.Lookups.Where(z => z.IsActive == true && z.IsDeleted == false && z.CategoryID == 1).ToList();
                    foreach (var item in reminders)
                    {
                        var remType = remTypes.Where(x => x.ID == item.TypeID).FirstOrDefault();
                        string TypeName = remType == null ? item.TypeID.ToString() : remType.NameEn;

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
                            EndDate = item.EndDate,
                            BreifDescription = item.BreifDescription,
                            Time = item.Time,
                            EmployeeID = item.EmployeeID,
                            TypeName = TypeName,
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
                RemindersEntities Entities = new RemindersEntities();


                Reminder entity = new Reminder();

                entity.ID = reminder.ID;
                entity.Name = reminder.Name;
                entity.IsActive = reminder.IsActive;
                entity.Image = null;
                entity.StartDate = reminder.StartDate.Value;
                entity.EndDate = reminder.EndDate;
                entity.BreifDescription = reminder.BreifDescription;
                entity.Time = reminder.Time;
                entity.CreatedOn = reminder.CreatedOn.Value;
                entity.CreatedBy = reminder.CreatedBy;
                entity.TypeID = LookupService.GetLookupIdByCode((int)Lookups.general);
                Entities.Reminders.Add(entity);
                Entities.SaveChanges();
                reminder.ID = entity.ID;
                foreach (var x in reminder.SelectedGroupsID)
                {
                    Entities.RemindersGroups.Add(new RemindersGroup { GroupID = x, ReminderID = entity.ID, CreatedOn = DateTime.Now, CreatedBy = 1, ModifyOn = DateTime.Now, ModifyBy = 1 });
                    Entities.SaveChanges();
                }






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
                RemindersEntities Entities = new RemindersEntities();

                var items = Entities.Reminders.Where(x => x.Email == reminder.Email).ToList();

                if (items.Count < 1)
                {
                    Reminder entity = new Reminder();

                    entity.ID = reminder.ID;
                    entity.Name = reminder.Name;
                    entity.Email = reminder.Email;
                    entity.BirthDate = reminder.BirthDate;
                    entity.PositionID = reminder.PositionID;
                    entity.IsActive = reminder.IsActive;
                    entity.Image = null;
                    entity.StartDate = reminder.StartDate.Value;
                    entity.EmployeeID = reminder.EmployeeID;
                    entity.CreatedBy = reminder.CreatedBy;
                    entity.CreatedOn = DateTime.Now;
                    entity.TypeID = LookupService.GetLookupIdByCode((int)Lookups.employee);

                    Entities.Reminders.Add(entity);
                    Entities.SaveChanges();

                    reminder.ID = entity.ID;
                }
                else
                {

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
        public static void EmployeeReminderUpdate(EmployeeRemindersViewModel reminder, HttpPostedFileBase image1)
        {
            try
            {
                RemindersEntities Entities = new RemindersEntities();


                Reminder entity = Entities.Reminders.Find(reminder.ID);

                entity.Name = reminder.Name;
                entity.Email = reminder.Email;
                entity.BirthDate = reminder.BirthDate;
                entity.PositionID = reminder.PositionID;
                entity.IsActive = reminder.IsActive;
                entity.Image = null;
                entity.ImagePath = reminder.ImagePath;
                entity.StartDate = reminder.StartDate.Value;
                entity.EmployeeID = reminder.EmployeeID;
                entity.ModifyBy = reminder.ModifyBy;
                entity.ModifyOn = reminder.ModifyOn;
                entity.CreatedBy = reminder.CreatedBy;
                entity.TypeID = LookupService.GetLookupIdByCode((int)Lookups.employee);

                Entities.SaveChanges();
            }

            catch (Exception ex)
            {
                throw ex;
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
                RemindersEntities Entities = new RemindersEntities();

                Reminder entity = Entities.Reminders.Find(reminder.ID);

                entity.Name = reminder.Name;
                entity.IsActive = reminder.IsActive;
                entity.StartDate = reminder.StartDate.Value;
                entity.EndDate = reminder.EndDate;
                entity.BreifDescription = reminder.BreifDescription;
                entity.Time = reminder.Time;
                entity.ModifyBy = reminder.ModifyBy;
                entity.ModifyOn = reminder.ModifyOn;
                entity.TypeID = LookupService.GetLookupIdByCode((int)Lookups.general);
                var list = entity.RemindersGroups;
                Entities.RemindersGroups.RemoveRange(list);
                Entities.SaveChanges();
                foreach (var x in reminder.SelectedGroupsID)//************??????
                {
                    Entities.RemindersGroups.Add(new RemindersGroup { GroupID = x, ReminderID = entity.ID, ModifyOn = DateTime.Now, ModifyBy = 1,CreatedOn= DateTime.Now ,CreatedBy=1});
                    Entities.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Dispose is for releasing "unmanaged" resources ,
        /// and if it's being called outside a finalizer, 
        /// for disposing other IDisposable objects it holds that are no longer useful.
        /// </summary>
        public void Dispose()
        {
            RemindersEntities Entities = new RemindersEntities();

            Entities.Dispose();
        }

        //public static RemindersGroupsViewModel RemindersGroupsGetByID(int id)
        //{
        //    using (RemindersEntities db = new RemindersEntities())
        //    {
        //        Group groups = db.Groups.Find(id);


        //        RemindersGroupsViewModel RemindersgroupInfo = new RemindersGroupsViewModel()
        //        {
        //            ID = groups.ID,
        //            GroupID = groups.ID,
        //            Name = groups.Name,
        //            ReminderID = groups.RemindersGroups.Select(x => x.ReminderID).FirstOrDefault()
        //        };
        //        RemindersgroupInfo.Grouplist = new List<GroupsViewModel>();

        //        foreach (var emp in groups.RemindersGroups)
        //        {
        //            RemindersgroupInfo.Grouplist.Add(new GroupsViewModel
        //            {
        //                Name = emp.Group.Name
        //            });
        //        }
        //        return RemindersgroupInfo;
        //    }
        //}


    }
}
