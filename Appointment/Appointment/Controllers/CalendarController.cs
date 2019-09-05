using Appointment.Business.Models;
using Appointment.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Controllers
{
    public class CalendarController : Controller
    {
        
        public ActionResult Index()
        {
            //var type = ReminderService.GetType(id);
            //if (type == LookupService.GetLookupIdByCode((int)Lookups.employee))
            return View(CalendarService.DisplayCurrentReminders());
        }
    }

    
}