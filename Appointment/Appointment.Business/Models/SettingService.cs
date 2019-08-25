using Appointment.DAL.Models;
using Appointment.ViewModel;
using Appointment.ViewModel.Enums;
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
                var DurationToRemindeLookupCategoriesID = db.LookUpCategories.Where(x => x.Code == ((int)LookupCategories.DurationToReminder).ToString()).FirstOrDefault().ID;
                var list = db.Lookups.Where(m => m.CategoryID == DurationToRemindeLookupCategoriesID).Select(m => new SelectListItem
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


                string BirthDayEmailTextKey = SettingsKeys.BirthDayEmailTextKey();
                string AnniversaryEmailTextKey = SettingsKeys.AnniversaryEmailTextKey();
                string BirthdayReminderKey = SettingsKeys.BirthdayReminderKey();
                string AnniversaryReminderKey = SettingsKeys.AnniversaryReminderKey();
                string EventReminderKey = SettingsKeys.EventReminderKey();
                string SendBirthdayKey = SettingsKeys.SendBirthdayKey();
                string SendAnniversaryKey = SettingsKeys.SendAnniversaryKey();
                string SendEventKey = SettingsKeys.SendEventKey();
                string UpComingReminderKey = SettingsKeys.UpComingReminderKey();
                string EmailAdminKey = SettingsKeys.EmailAdminKey();
                string EmailSenderKey = SettingsKeys.EmailSenderKey();
                string PasswordSenderKey = SettingsKeys.PasswordSenderKey();
                string smtpaddressKey = SettingsKeys.smtpaddressKey();
                string portnumberKey = SettingsKeys.portnumberKey();



                var set0 = Entities.Settings.Where(x => x.Key == BirthDayEmailTextKey).FirstOrDefault();
                set0.Values = settingviewmodel.settingsView.BirthDayEmailText;
                set0.Description = settingviewmodel.settingsView.BirthDayEmailText;
                set0.CreatedOn = DateTime.Now;

                var set1 = Entities.Settings.Where(x => x.Key == AnniversaryEmailTextKey).FirstOrDefault();
                set1.Values = settingviewmodel.settingsView.AnniversaryEmailText;
                set1.Description = settingviewmodel.settingsView.AnniversaryEmailText;
                set1.CreatedOn = DateTime.Now;


                var set2 = Entities.Settings.Where(x => x.Key == BirthdayReminderKey).FirstOrDefault();
                set2.Values = settingviewmodel.BirthdayReminderID.ToString();
                var Lookup2 = Entities.Lookups.Find(settingviewmodel.BirthdayReminderID);
                set2.Description = Lookup2.Description.ToString();
                set2.CreatedOn = DateTime.Now;


                var set3 = Entities.Settings.Where(x => x.Key == AnniversaryReminderKey).FirstOrDefault();
                set3.Values = settingviewmodel.AnniversaryReminderID.ToString();
                var Lookup3 = Entities.Lookups.Find(settingviewmodel.AnniversaryReminderID);
                set3.Description = Lookup3.Description.ToString();
                set3.CreatedOn = DateTime.Now;

                var set4 = Entities.Settings.Where(x => x.Key == EventReminderKey).FirstOrDefault();
                set4.Values = settingviewmodel.EventReminderID.ToString();
                var Lookup4 = Entities.Lookups.Find(settingviewmodel.EventReminderID);
                set4.Description = Lookup4.Description.ToString();
                set4.CreatedOn = DateTime.Now;

                var set5 = Entities.Settings.Where(x => x.Key == SendBirthdayKey).FirstOrDefault();
                set5.Values = settingviewmodel.SendBirthdayID.ToString();
                var Lookup5 = Entities.Lookups.Find(settingviewmodel.SendBirthdayID);
                set5.Description = Lookup5.Description.ToString();
                set5.CreatedOn = DateTime.Now;

                var set6 = Entities.Settings.Where(x => x.Key == SendAnniversaryKey).FirstOrDefault();
                set6.Values = settingviewmodel.SendAnniversaryID.ToString();
                var Lookup6 = Entities.Lookups.Find(settingviewmodel.SendAnniversaryID);
                set6.Description = Lookup6.Description.ToString();
                set6.CreatedOn = DateTime.Now;

                var set7 = Entities.Settings.Where(x => x.Key == SendEventKey).FirstOrDefault();
                set7.Values = settingviewmodel.SendEventID.ToString();
                var Lookup7 = Entities.Lookups.Find(settingviewmodel.SendEventID);
                set7.Description = Lookup7.Description.ToString();
                set7.CreatedOn = DateTime.Now;

                var set8 = Entities.Settings.Where(x => x.Key == UpComingReminderKey).FirstOrDefault();
                set8.Values = settingviewmodel.UpComingReminderID.ToString();
                var Lookup8 = Entities.Lookups.Find(settingviewmodel.UpComingReminderID);
                set8.Description = Lookup8.Description.ToString();
                set8.CreatedOn = DateTime.Now;

                var set15 = Entities.Settings.Where(x => x.Key == EmailAdminKey).FirstOrDefault();
                set15.Values = settingviewmodel.settingsView.EmailAdmin.ToString();
                set15.Description = settingviewmodel.settingsView.EmailAdmin.ToString();
                set15.CreatedOn = DateTime.Now;

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
            string BirthDayEmailTextKey = SettingsKeys.BirthDayEmailTextKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == BirthDayEmailTextKey).FirstOrDefault(); 
                return setting.Description;
            }
        }
        public static string AnniversaryEmailText()
        {
            string AnniversaryEmailTextKey = SettingsKeys.AnniversaryEmailTextKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == AnniversaryEmailTextKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string BirthdayReminder()
        {
            string BirthdayReminderKey = SettingsKeys.BirthdayReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == BirthdayReminderKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string AnniversaryReminder()
        {
            string AnniversaryReminderKey = SettingsKeys.AnniversaryReminderKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == AnniversaryReminderKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string EventReminder()
        {
            string EventReminderKey = SettingsKeys.EventReminderKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == EventReminderKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendBirthday()
        {
            string SendBirthdayKey = SettingsKeys.SendBirthdayKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendBirthdayKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendAnniversary()
        {
            string SendAnniversaryKey = SettingsKeys.SendAnniversaryKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendAnniversaryKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string SendEvent()
        {
            string SendEventKey = SettingsKeys.SendEventKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendEventKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string UpComingReminder()
        {
            string UpComingReminderKey = SettingsKeys.UpComingReminderKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == UpComingReminderKey).FirstOrDefault();
                return setting.Description;
            }
        }
     
        public static string EmailAdmin()
        {
            string EmailAdminKey = SettingsKeys.EmailAdminKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == EmailAdminKey).FirstOrDefault();
                return setting.Description;
            }
        }

        public static string EmailSender()
        {
            string EmailSenderKey = SettingsKeys.EmailSenderKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == EmailSenderKey).FirstOrDefault();
                return setting.Description;
            }
        }
        public static string PasswordSender()
        {
            string PasswordSenderKey = SettingsKeys.PasswordSenderKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == PasswordSenderKey).FirstOrDefault();
                return setting.Description;
            }
        }

        public static string smtpaddress()
        {
            string smtpaddressKey = SettingsKeys.smtpaddressKey();
            
            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == smtpaddressKey).FirstOrDefault();
                return setting.Description;
            }
        }

        public static string portnumber()
        {
            string portnumberKey = SettingsKeys.portnumberKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == portnumberKey).FirstOrDefault();
                return setting.Description;
            }
        }
        

        //----------------------------------------ddl selected item-------------------------------------//
        public static string BirthdayReminderID()
        {
            string BirthdayReminderKey = SettingsKeys.BirthdayReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == BirthdayReminderKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string AnniversaryReminderID()
        {
            string AnniversaryReminderKey = SettingsKeys.AnniversaryReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == AnniversaryReminderKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string EventReminderID()
        {
            string EventReminderKey = SettingsKeys.EventReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == EventReminderKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string SendBirthdayID()
        {
            string SendBirthdayKey = SettingsKeys.SendBirthdayKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendBirthdayKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string SendAnniversaryID()
        {
            string SendAnniversaryKey = SettingsKeys.SendAnniversaryKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendAnniversaryKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string SendEventID()
        {
            string SendEventKey = SettingsKeys.SendEventKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == SendEventKey).FirstOrDefault();
                return setting.Values;
            }
        }
        public static string UpComingReminderID()
        {
            string UpComingReminderKey = SettingsKeys.UpComingReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var setting = db.Settings.Where(x => x.Key == UpComingReminderKey).FirstOrDefault();
                return setting.Values;
            }
        }





        //----------------------------------------ddl NameEn Text-------------------------------------//
        public static string BirthdayReminderEN()
        {
            string BirthdayReminderKey = SettingsKeys.BirthdayReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == BirthdayReminderKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string AnniversaryReminderEN()
        {
            string AnniversaryReminderKey = SettingsKeys.AnniversaryReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == AnniversaryReminderKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string EventReminderEN()
        {
            string EventReminderKey = SettingsKeys.EventReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == EventReminderKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string SendBirthdayEN()
        {
            string SendBirthdayKey = SettingsKeys.SendBirthdayKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == SendBirthdayKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string SendAnniversaryEN()
        {
            string SendAnniversaryKey = SettingsKeys.SendAnniversaryKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == SendAnniversaryKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string SendEventEN()
        {
            string SendEventKey = SettingsKeys.SendEventKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == SendEventKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
            }
        }
        public static string UpComingReminderEN()
        {
            string UpComingReminderKey = SettingsKeys.UpComingReminderKey();

            using (RemindersEntities db = new RemindersEntities())
            {
                var Settings = db.Settings.Where(x => x.Key == UpComingReminderKey).FirstOrDefault();
                var value = Convert.ToInt32(Settings.Values);
                var Lookups = db.Lookups.Where(x => x.ID == value).FirstOrDefault();
                return Lookups.NameEn;
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