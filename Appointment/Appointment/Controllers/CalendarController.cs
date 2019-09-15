using Appointment.Business.Models;
using Appointment.ViewModel.Enums;
using Appointment.ViewModel.Models;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Controllers
{
    public class CalendarController : Controller
    {


        public ActionResult Index()
        {
            var List =  CalendarService.DisplayCurrentReminders(); 
            return View(List);
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, string Date, string Name)
        {
            DateTime? dt = null;
            if (!string.IsNullOrEmpty(Date))
            {
                dt=DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            var data = CalendarService.DisplayFilteredReminders(dt, Name);

            var result = new DataSourceResult
            {
                Data = data,
            };
            return Json(result, JsonRequestBehavior.AllowGet);


        }



     //////////////////////////////////////////////////////////////////

        public ViewResult ReminderDetails(int id)
        {
            var type = ReminderService.GetType(id);
            if (type == LookupService.GetLookupIdByCode((int)Lookups.employee))
            {
                EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
                obj = ReminderService.EmployeeRemindersGetByID(id);
                obj.Positions = ReminderService.GetPositions();
                return View("Details", obj);
            }
            else
            {
                GeneralRemindersViewModel obj = new GeneralRemindersViewModel();
                obj = ReminderService.generalRemindersGetByID(id);
                return View("GeneralDetails", obj);
            }


        }

    }
}



