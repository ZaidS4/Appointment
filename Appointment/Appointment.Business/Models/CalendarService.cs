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
        public static List<CalendarViewModel> GetCurrentReminders()
        {
            List<CalendarViewModel> reminderViews = new List<CalendarViewModel>();

            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.UpComingReminder()));
                    var ti = DateTime.Now.Date.AddDays(0);

                    var reminders = db.Reminders.ToList();
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
                                    Name = item.Name + "  Birthday " ,
                                    TheDate = item.BirthDate

                                });
                            }
                            if (item.StartDate.Value.Month <= t.Month && item.StartDate.Value.Day <= t.Day && item.StartDate.Value.Month >= ti.Month && item.StartDate.Value.Day >= ti.Day)
                            {
                                reminderViews.Add(new CalendarViewModel
                                {
                                    ID = item.ID,
                                    Name = item.Name + "  Anniversary ",
                                    TheDate = item.StartDate


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
                                    TheDate = item.StartDate,

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
           


        public static List<CalendarViewModel> DisplayCurrentReminders()
        {
            return GetCurrentReminders();
        }


        ////////////////////////////////////////////////////////

        public void Dispose()
        {
            RemindersEntities Entities = new RemindersEntities();

            Entities.Dispose();
        }
    }
}

    //for (int i = Convert.ToInt32(SettingService.UpComingReminder()); i >= 0; i--)
    //{


    //}
    //return reminderViews.Where(x => x.TheDate.Value.Day == t.Day && x.TheDate.Value.Month == t.Month).ToList();
    // return reminderViews.FindAll(x => x.TheDate == DateTime.Today || x => x.BirthDate==DateTime.Now);

//////////
////////////////////////////////////////////////////////








//var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.UpComingReminder()));
//List<RemindersViewModel> Calendar = new List<RemindersViewModel>();
//    for (int i = Convert.ToInt32(SettingService.UpComingReminder()); i >= 0; i--)
//    {


//    }
/*.Where(x => x.TheDate.Value.Day == t.Day && x.TheDate.Value.Month == t.Month).ToList()*/

