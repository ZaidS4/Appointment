using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Appointment.Business.Models
{
    public class SettingService
    {
        public static List<SelectListItem> GetDayBefore()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var list = db.Lookups.Where(m => m.CategoryID == 8).Select(m => new SelectListItem
                {
                    Value = m.ID.ToString(),
                    Text = m.NameEn,
                }).ToList();
                return list;
            }
        }

        

        public static void Save(SettingsViewModel settingviewmodel)
        {
            try
            {
                RemindersEntities Entities = new RemindersEntities();


                var set0 = Entities.Settings.Where(x => x.Key == ("BirthDayEmailText")).FirstOrDefault();
                set0.Values = settingviewmodel.settingsView.BirthDayEmailText;
                set0.Description = settingviewmodel.settingsView.BirthDayEmailText;
                set0.CreatedOn = DateTime.Now;

                var set1 = Entities.Settings.Where(x => x.Key == ("AnniversaryEmailText")).FirstOrDefault();
                set1.Values = settingviewmodel.settingsView.AnniversaryEmailText;
                set1.Description = settingviewmodel.settingsView.AnniversaryEmailText;
                set1.CreatedOn = DateTime.Now;


                var set2 = Entities.Settings.Where(x => x.Key == ("BirthdayReminder")).FirstOrDefault();
                set2.Values = settingviewmodel.BirthdayReminderID.ToString();
                var Lookup2 = Entities.Lookups.Find(settingviewmodel.BirthdayReminderID);
                set2.Description = Lookup2.Description.ToString();
                set2.CreatedOn = DateTime.Now;


                var set3 = Entities.Settings.Where(x => x.Key == ("AnniversaryReminder")).FirstOrDefault();
                set3.Values = settingviewmodel.AnniversaryReminderID.ToString();
                var Lookup3 = Entities.Lookups.Find(settingviewmodel.AnniversaryReminderID);
                set3.Description = Lookup3.Description.ToString();
                set3.CreatedOn = DateTime.Now;

                var set4 = Entities.Settings.Where(x => x.Key == ("EventReminder")).FirstOrDefault();
                set4.Values = settingviewmodel.EventReminderID.ToString();
                var Lookup4 = Entities.Lookups.Find(settingviewmodel.EventReminderID);
                set4.Description = Lookup4.Description.ToString();
                set4.CreatedOn = DateTime.Now;

                var set5 = Entities.Settings.Where(x => x.Key == ("SendBirthday")).FirstOrDefault();
                set5.Values = settingviewmodel.SendBirthdayID.ToString();
                var Lookup5 = Entities.Lookups.Find(settingviewmodel.SendBirthdayID);
                set5.Description = Lookup5.Description.ToString();
                set5.CreatedOn = DateTime.Now;

                var set6 = Entities.Settings.Where(x => x.Key == ("SendAnniversary")).FirstOrDefault();
                set6.Values = settingviewmodel.SendAnniversaryID.ToString();
                var Lookup6 = Entities.Lookups.Find(settingviewmodel.SendAnniversaryID);
                set6.Description = Lookup6.Description.ToString();
                set6.CreatedOn = DateTime.Now;

                var set7 = Entities.Settings.Where(x => x.Key == ("SendEvent")).FirstOrDefault();
                set7.Values = settingviewmodel.SendEventID.ToString();
                var Lookup7 = Entities.Lookups.Find(settingviewmodel.SendEventID);
                set7.Description = Lookup7.Description.ToString();
                set7.CreatedOn = DateTime.Now;

                var set8 = Entities.Settings.Where(x => x.Key == ("UpComingReminder")).FirstOrDefault();
                set8.Values = settingviewmodel.UpComingReminderID.ToString();
                var Lookup8 = Entities.Lookups.Find(settingviewmodel.UpComingReminderID);
                set8.Description = Lookup8.Description.ToString();
                set8.CreatedOn = DateTime.Now;

                //var set9 = Entities.Settings.Where(x => x.Key == ("RemindMeAt")).FirstOrDefault();
                //set9.Values = settingviewmodel.settingsView.RemindMeAt.ToString();
                //set9.Description = settingviewmodel.settingsView.RemindMeAt.ToString();
                //set9.CreatedOn = DateTime.Now;

                //var set10 = Entities.Settings.Where(x => x.Key == ("SendEmailAt")).FirstOrDefault();
                //set10.Values = settingviewmodel.settingsView.SendEmailAt.ToString();
                //set10.Description = settingviewmodel.settingsView.SendEmailAt.ToString();
                //set10.CreatedOn = DateTime.Now;


                var set11 = Entities.Settings.Where(x => x.Key == ("EmailSender")).FirstOrDefault();
                set11.Values = settingviewmodel.settingsView.EmailSender.ToString();
                set11.Description = settingviewmodel.settingsView.EmailSender.ToString();
                set11.CreatedOn = DateTime.Now;

                var set12 = Entities.Settings.Where(x => x.Key == ("PasswordSender")).FirstOrDefault();
                set12.Values = settingviewmodel.settingsView.PasswordSender.ToString();
                set12.Description = settingviewmodel.settingsView.PasswordSender.ToString();
                set12.CreatedOn = DateTime.Now;

                var set13 = Entities.Settings.Where(x => x.Key == ("smtpaddress")).FirstOrDefault();
                set13.Values = settingviewmodel.settingsView.smtpaddress.ToString();
                set13.Description = settingviewmodel.settingsView.smtpaddress.ToString();
                set13.CreatedOn = DateTime.Now;

                var set14 = Entities.Settings.Where(x => x.Key == ("portnumber")).FirstOrDefault();
                set14.Values = settingviewmodel.settingsView.portnumber.ToString();
                set14.Description = settingviewmodel.settingsView.portnumber.ToString();
                set14.CreatedOn = DateTime.Now;

                Entities.SaveChanges();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Retrive Data from Database
        public static string BirthDayEmailText()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "BirthDayEmailText").FirstOrDefault(); 
                return setting.Description;
            }
        }
        public static string AnniversaryEmailText()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "AnniversaryEmailText").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string BirthdayReminder()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "BirthdayReminder").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string AnniversaryReminder()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "AnniversaryReminder").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string EventReminder()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "EventReminder").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendBirthday()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "SendBirthday").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendAnniversary()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "SendAnniversary").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendEvent()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "SendEvent").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string UpComingReminder()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "UpComingReminder").FirstOrDefault();
                return setting.Description;
            }
        }
        //public static string RemindMeAt()
        //{
        //    using (RemindersEntities db = new RemindersEntities())
        //    {
        //        var setting = db.Settings.Where(x => x.Key == "RemindMeAt").FirstOrDefault();
        //        return setting.Description;
        //    }
        //}
        //public static string SendEmailAt()
        //{
        //    using (RemindersEntities db = new RemindersEntities())
        //    {
        //        var setting = db.Settings.Where(x => x.Key == "SendEmailAt").FirstOrDefault();
        //        return setting.Description;
        //    }
        //}
        public static string EmailSender()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "EmailSender").FirstOrDefault();
                return setting.Description;
            }
        }
        public static string PasswordSender()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "PasswordSender").FirstOrDefault();
                return setting.Description;
            }
        }

        public static string smtpaddress()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "smtpaddress").FirstOrDefault();
                return setting.Description;
            }
        }

        public static string portnumber()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == "portnumber").FirstOrDefault();
                return setting.Description;
            }
        }
    }
}
//-------------------------------------------CommentArea----------------------------------------//
//public static List<SettingsViewModel> GetSettings()
//{
//    List<SettingsViewModel> settingsViews = new List<SettingsViewModel>();

//    try
//    {
//        using (RemindersEntities db = new RemindersEntities())
//        {
//            var Settings = db.Settings.ToList();

//            foreach (var item in Settings)
//            {

//                settingsViews.Add(new SettingsViewModel
//                {
//                    ID = item.ID,
//                    Key = item.Key,
//                    Description = item.Description
//                });
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    return settingsViews;
//}