using Appointment.Business.Job;
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
            obj.settingsView = new SettingsView();
            obj.settingsView.BirthDayEmailText = SettingService.BirthDayEmailText();
            obj.settingsView.AnniversaryEmailText = SettingService.AnniversaryEmailText();
            obj.settingsView.EmailAdmin = SettingService.EmailAdmin();

            //get item selected value
            obj.BirthdayReminderID = Convert.ToInt32(SettingService.BirthdayReminderID());
            obj.AnniversaryReminderID = Convert.ToInt32(SettingService.AnniversaryReminderID());
            obj.EventReminderID = Convert.ToInt32(SettingService.EventReminderID());
            obj.SendBirthdayID = Convert.ToInt32(SettingService.SendBirthdayID());
            obj.SendAnniversaryID = Convert.ToInt32(SettingService.SendAnniversaryID());
            obj.SendEventID = Convert.ToInt32(SettingService.SendEventID());
            obj.UpComingReminderID = Convert.ToInt32(SettingService.UpComingReminderID());



            //get list data from database(lookup table)

            obj.BirthdayReminder = SettingService.GetDayBefore();
            obj.AnniversaryReminder = SettingService.GetDayBefore();
            obj.EventReminder = SettingService.GetDayBefore();
            obj.SendBirthday = SettingService.GetDayBefore();
            obj.SendAnniversary = SettingService.GetDayBefore();
            obj.SendEvent = SettingService.GetDayBefore();
            obj.UpComingReminder = SettingService.GetDayBefore();


            return View(obj);
            //return null;
        }
        [HttpPost]
        public ActionResult Index(SettingsViewModel sv)
        {
           
                SettingService.Save(sv);
           
            return RedirectToAction("Index","Settings");
        }
    }
}