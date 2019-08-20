using Appointment.Business.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {



            SettingsViewModel obj = new SettingsViewModel();
            obj.BirthdayReminder = SettingService.GetDayBefore();
            obj.AnniversaryReminder = SettingService.GetDayBefore();
            obj.EventReminder = SettingService.GetDayBefore();
            obj.SendBirthday = SettingService.GetDayBefore();
            obj.SendAnniversary = SettingService.GetDayBefore();
            obj.SendEvent = SettingService.GetDayBefore();
            obj.UpComingReminder = SettingService.GetDayBefore();

            return View(obj);
        }
        [HttpPost]
        public ActionResult Index(SettingsViewModel sv)
        {
           
                SettingService.Save(sv);
           
            return RedirectToAction("Index","Settings");
        }
    }
}