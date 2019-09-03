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
                    var reminders = db.Reminders.ToList();
                    foreach (var item in reminders)
                    {
                        //LookupService.GetLookupIdByCode((int)Lookups.employee);
                        int tId= ((int)Lookups.employee);

                        if (item.TypeID == tId) /* "Employee"*/
                        {
                            reminderViews.Add(new CalendarViewModel
                            {
                                ID = item.ID,
                                Name = item.Name,
                                TheDate =item.BirthDate
                                //Day = item.BirthDate.Value.Day,
                                //Month=item.BirthDate.Value.Month
                            });
                            reminderViews.Add(new CalendarViewModel
                            {
                                ID = item.ID,
                                Name = item.Name,
                                TheDate = item.StartDate
                                //Day=item.StartDate.Day,
                                //Month = item.StartDate.Month

                            });                         
                        }
                        else
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
            catch (Exception ex)
            {
                throw ex;
            }
            return reminderViews/*.Where(x => x.TheDate == DateTime.Today || x.BirthDate == DateTime.Today).*/.ToList();
            // return reminderViews.FindAll(x => x.TheDate == DateTime.Today || x => x.BirthDate==DateTime.Now);
        }
        //////////
        ////////////////////////////////////////////////////////


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

