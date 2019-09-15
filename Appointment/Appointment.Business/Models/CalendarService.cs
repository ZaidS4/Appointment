using Appointment.DAL.Models;
using Appointment.ViewModel.Enums;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Appointment.Business.Models
{
    public class CalendarService : IDisposable
    {
        public static List<CalendarViewModel> DisplayCurrentReminders()
        {
            List<CalendarViewModel> reminderViews = new List<CalendarViewModel>();

            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.UpComingReminder()));
                    var ti = DateTime.Now.Date.AddDays(0);

                    var reminders = db.Reminders.Where(x => x.IsActive == true).ToList();
                    foreach (var item in reminders)
                    {
                        //LookupService.GetLookupIdByCode((int)Lookups.employee);
                        //int tId = ((int)Lookups.employee);
                        int emploeeLookupID = db.Lookups.Where(x => x.Code == ((int)Lookups.employee).ToString()).FirstOrDefault().ID;
                        if (item.TypeID == emploeeLookupID) /* "Employee"*/
                        {
                            if (item.BirthDate.Value.Month <= t.Month && item.BirthDate.Value.Day <= t.Day && item.BirthDate.Value.Month >= ti.Month && item.BirthDate.Value.Day >= ti.Day)
                            {
                                reminderViews.Add(new CalendarViewModel
                                {
                                    ID = item.ID,
                                    Name = item.Name + "  Birthday ",
                                    TheDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToString("MM/dd/yyyy") : ""

                                });
                            }
                            if (item.StartDate.Value.Month <= t.Month && item.StartDate.Value.Day <= t.Day && item.StartDate.Value.Month >= ti.Month && item.StartDate.Value.Day >= ti.Day)
                            {
                                reminderViews.Add(new CalendarViewModel
                                {
                                    ID = item.ID,
                                    Name = item.Name + "  Anniversary ",
                                    TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""


                                });
                            }
                        }
                        else
                        {
                            if (item.StartDate.Value.Date <= t.Date && item.StartDate.Value.Date >= ti.Date)
                            {
                                reminderViews.Add(new CalendarViewModel
                                {
                                    ID = item.ID,
                                    Name = item.Name,
                                    TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""

                                });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reminderViews;
        }




        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<CalendarViewModel> DisplayFilteredReminders(DateTime? date, String Name)
        {
            List<CalendarViewModel> reminderViews = new List<CalendarViewModel>();

            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {

                    var reminders = db.Reminders.Where(x => x.IsActive == true).ToList();
                    
                        foreach (var item in reminders)
                        {

                            int emploeeLookupID = db.Lookups.Where(x => x.Code == ((int)Lookups.employee).ToString()).FirstOrDefault().ID;
                            //if (item.TypeID == emploeeLookupID) 
                            if (date != null && Name == "")
                            {
                                if (item.TypeID == emploeeLookupID)
                                {
                                    if (item.BirthDate.Value.Month == date.Value.Month && item.BirthDate.Value.Day == date.Value.Day)
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Birthday ",
                                            TheDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                    if (item.StartDate.Value.Month == date.Value.Month && item.StartDate.Value.Day == date.Value.Day)
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Anniversary ",
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""


                                        });
                                    }
                                }
                                else
                                {
                                    if (item.StartDate.Value.Date == date.Value.Date)
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name,
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                }
                            }
                            else if (date == null && Name != "")
                            {
                                //if ((item.StartDate.Value.Date == date.Date && item.StartDate.Value.Date == date.Date)|| item.Name.Contains(Name))
                                if (item.TypeID == emploeeLookupID)
                                {
                                    if (item.Name.Contains(Name))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Birthday ",
                                            TheDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                    if (item.Name.Contains(Name))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Anniversary ",
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""


                                        });
                                    }
                                }
                                else
                                {
                                    if (item.Name.Contains(Name))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name,
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                }
                            }
                            else if (date != null && Name != "")
                            {
                                if (item.TypeID == emploeeLookupID)
                                {
                                    if ((item.Name.Contains(Name)) && (item.BirthDate.Value.Month == date.Value.Month && item.BirthDate.Value.Day == date.Value.Day))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Birthday ",
                                            TheDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                    if ((item.Name.Contains(Name)) && (item.StartDate.Value.Month == date.Value.Month && item.StartDate.Value.Day == date.Value.Day))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name + "  Anniversary ",
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""


                                        });
                                    }
                                }
                                else
                                {
                                    if ((item.Name.Contains(Name)) && (item.StartDate.Value.Month == date.Value.Month && item.StartDate.Value.Day == date.Value.Day))
                                    {
                                        reminderViews.Add(new CalendarViewModel
                                        {
                                            ID = item.ID,
                                            Name = item.Name,
                                            TheDate = item.StartDate.HasValue ? item.StartDate.Value.ToString("MM/dd/yyyy") : ""

                                        });
                                    }
                                }

                            
                        }
                        else
                        {
                            reminderViews = DisplayCurrentReminders();

                        }

                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reminderViews;
        }


        ////////////////////////////////////////////////////////

        public void Dispose()
        {
            RemindersEntities Entities = new RemindersEntities();

            Entities.Dispose();
        }
    }
}
