using Appointment.Business.Models;
using Appointment.DAL.Models;
using Appointment.Resource;
using Appointment.ViewModel.Models;
using Ninject;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Appointment.ViewModel.Enums;
using Microsoft.Exchange.WebServices.Data;
using System.Web.Configuration;

namespace Appointment.Business.Job
{
    public class MailSender : IJob
    {
        private readonly IAppointmentRepository apR;
        Ninject.IKernel kernel = new StandardKernel();

        //-----------------------------------------------/////////////----------------------------------------------//
        public MailSender()
        {
            apR = DependencyResolver.Current.GetService<IAppointmentRepository>();
        }

        //----------------------------------------------------Job Execute----------------------------------------------------//
        public void Execute(IJobExecutionContext context)
        {


            try
            {
                RemindersEntities db = new RemindersEntities();
                int emploeeLookupID = db.Lookups.Where(x => x.Code == ((int)Lookups.employee).ToString()).FirstOrDefault().ID;
                int generalLookupID = db.Lookups.Where(x => x.Code == ((int)Lookups.general).ToString()).FirstOrDefault().ID;
                List<RemindersViewModel> BirthDate = new List<RemindersViewModel>();
                BirthDate = db.Reminders.Where(x => x.TypeID == emploeeLookupID).Select(x => new RemindersViewModel { Name = x.Employee.Name, BirthDate = x.BirthDate, Email = x.Employee.Email, Image = x.Image }).ToList();//

                //Anniversary
                List<RemindersViewModel> EmployeeStartDate = new List<RemindersViewModel>();
                EmployeeStartDate = db.Reminders.Where(x => x.TypeID == emploeeLookupID).Select(x => new RemindersViewModel { Name = x.Employee.Name, StartDate = x.StartDate, Email = x.Employee.Email, ImagePath = x.ImagePath, ID = x.ID }).ToList();

                //general
                List<RemindersViewModel> startDate = new List<RemindersViewModel>();
                startDate = db.Reminders.Where(x => x.TypeID == generalLookupID).Select(x => new RemindersViewModel { StartDate = x.StartDate, Name = x.Name, BreifDescription = x.BreifDescription, ID = x.ID, Time = x.Time, EndDate = x.EndDate }).ToList();


                //For Send Email
                foreach (var item in BirthDate)
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.SendBirthday()));
                    if (item.BirthDate.HasValue == true)
                    {
                        if (t.Day == item.BirthDate.Value.Day && t.Month == item.BirthDate.Value.Month)
                        {
                            bool res = SendEmailBirthday(item.Name, item.Email);//
                        }
                    }
                }
                foreach (var item in EmployeeStartDate)
                {
                    RemindersEntities dbESD = new RemindersEntities();
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.SendAnniversary()));
                    if (item.StartDate.HasValue == true)
                    {
                        if (t.Day == item.StartDate.Value.Day && t.Month == item.StartDate.Value.Month)
                        {
                            bool res = SendEmailEmployeeStartDate(item.Name, item.Email, item.ImagePath, item.ID);
                        }
                    }

                }
                foreach (var item in startDate)
                {
                    RemindersEntities dbSD = new RemindersEntities();
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.SendEvent()));
                    if (item.StartDate.HasValue == true)
                    {
                        if (t.Day == item.StartDate.Value.Day && t.Month == item.StartDate.Value.Month)
                        {
                            bool res = SendEmailGeneral(item.Name, item.BreifDescription, item.ID, item.Time.Value, item.EndDate.Value, item.StartDate.Value);
                        }
                    }

                }


                //For Send Reminder
                foreach (var item in BirthDate)
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.BirthdayReminder()));
                    if (item.BirthDate.HasValue == true)
                    {
                        if (t.Day == item.BirthDate.Value.Day && t.Month == item.BirthDate.Value.Month)
                        {
                            bool res = RemindEmailBirthday(item.Name, item.BirthDate.Value);
                        }
                    }
                }
                foreach (var item in EmployeeStartDate)
                {
                    RemindersEntities dbESD = new RemindersEntities();
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.SendAnniversary()));
                    if (item.StartDate.HasValue == true)
                    {
                        if (t.Day == item.StartDate.Value.Day && t.Month == item.StartDate.Value.Month)
                        {
                            bool res = RemindEmailEmployeeStartDate(item.Name, item.StartDate.Value);
                        }
                    }

                }
                foreach (var item in startDate)
                {
                    RemindersEntities dbSD = new RemindersEntities();
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.EventReminder()));
                    if (item.StartDate.HasValue == true)
                    {
                        if (t.Day == item.StartDate.Value.Day && t.Month == item.StartDate.Value.Month)
                        {
                            bool res = RemindEmailGeneral(item.Name, item.BreifDescription, item.StartDate.Value, item.Time.Value);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------Send Email Function Birthday-----------------------------------------//
        public bool SendEmailBirthday(string name, string email)//
        {
            string ImagePath = "http://localhost//Appointment.Web//./img/happy-birthday.jpg";
            string bod = "<html><body><img src='"
                    + ImagePath + "'></body></html>";

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
            service.Url =new Uri ("https://mail.sssprocess.com/EWS/Exchange.asmx");
            EmailMessage emailMessage = new EmailMessage(service);
            emailMessage.Subject = "Have a wonderfull Birthday";
            emailMessage.Body = SettingService.BirthDayEmailText() + bod;
            emailMessage.ToRecipients.Add(email);
            emailMessage.SendAndSaveCopy();

            return true;
        }
        //---------------------------------------------Send Email Function Anniversary--------------------------------------//
        public bool SendEmailEmployeeStartDate(string name, string email, string ImagePath, int Id)
        {
            List<string> Data = new List<string>();
            using (RemindersEntities db2 = new RemindersEntities())
            {
                var filter1 = db2.Reminders.Where(x => x.ID == Id).Select(x => x.ID).ToList();
                var filter2 = db2.RemindersGroups.Where(x => filter1.Contains(x.ReminderID)).Select(x => x.GroupID).Distinct().ToList();
                Data = db2.EmployeesGroups.Where(s => filter2.Contains(s.GroupID)).Select(s => s.Employee.Email).ToList();
            }
            ImagePath = "http://localhost//Appointment.Web//" + ImagePath.Replace("~", ".");
            string bod = "<html><body><img src='" + ImagePath + "'></body></html>";
            foreach (var item in Data)
            {
                

                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
                service.Url = new Uri("https://mail.sssprocess.com/EWS/Exchange.asmx");
                EmailMessage emailMessage = new EmailMessage(service);
                emailMessage.Subject = name + " Happy Work Anniversary ";
                if (ImagePath == null)
                {
                    emailMessage.Body = SettingService.AnniversaryEmailText();
                }
                else
                {
                    emailMessage.Body = SettingService.AnniversaryEmailText() + bod;
                }
                emailMessage.ToRecipients.Add(item);
                emailMessage.SendAndSaveCopy();
                

            }
            return true;



        }
        //---------------------------------------------Send Email Function General------------------------------------------//
        public bool SendEmailGeneral(string name, string BreifD, int Id, TimeSpan Timespan, DateTime enddate, DateTime startdate)
        {


            List<string> Data = new List<string>();
            using (RemindersEntities db2 = new RemindersEntities())
            {
                var filter1 = db2.Reminders.Where(x => x.ID == Id).Select(x => x.ID).ToList();
                var filter2 = db2.RemindersGroups.Where(x => filter1.Contains(x.ReminderID)).Select(x => x.GroupID).Distinct().ToList();
                Data = db2.EmployeesGroups.Where(s => filter2.Contains(s.GroupID)).Select(s => s.Employee.Email).ToList();
            }

            foreach (var item in Data)
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
                service.Url = new Uri("https://mail.sssprocess.com/EWS/Exchange.asmx");
                EmailMessage emailMessage = new EmailMessage(service);
                emailMessage.Subject = name;
                DateTime time = DateTime.Today.Add(Timespan);
                string displayTime = time.ToString("hh:mm tt");
                emailMessage.Body = "Title : " + name + "<br>" + "Start date : " + startdate.Date.ToString("MM/dd/yyyy") + "<br>" + "End date : " + enddate.Date.ToString("MM/dd/yyyy") + "<br>" + "Description : " + BreifD + "<br>" + "Time  : " + displayTime;
                emailMessage.ToRecipients.Add(item);
                emailMessage.SendAndSaveCopy();
               
            }

            return true;
        }

        //---------------------------------------------Send Reminder Function Birthday-----------------------------------------//
        public bool RemindEmailBirthday(string name, DateTime birthdate)
        {
            string ImagePath = "http://localhost//Appointment.Web//./img/happy-birthday.jpg";
            string bod = "<html><body><img src='"
                    + ImagePath + "'></body></html>";

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
            service.Url = new Uri("https://mail.sssprocess.com/EWS/Exchange.asmx");
            EmailMessage emailMessage = new EmailMessage(service);
            emailMessage.Subject = name + " birthday";
            emailMessage.Body = "This is a Birthday Reminder email for " + name + " at " + birthdate.Date.ToString("MM/dd/yyyy");
            emailMessage.ToRecipients.Add(SettingService.EmailAdmin());
            emailMessage.SendAndSaveCopy();

            return true;
          
        }
        //---------------------------------------------Send Reminder Function Anniversary--------------------------------------//
        public bool RemindEmailEmployeeStartDate(string name, DateTime StartDate)
        {

            using (MailMessage mail = new MailMessage())
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
                service.Url = new Uri("https://mail.sssprocess.com/EWS/Exchange.asmx");
                EmailMessage emailMessage = new EmailMessage(service);
                emailMessage.Subject = name + " Anniversary";
                emailMessage.Body = "This is an Anniversary Reminder email for " + name + " at " + StartDate.Date.ToString("MM/dd/yyyy");
                emailMessage.ToRecipients.Add(SettingService.EmailAdmin());
                emailMessage.SendAndSaveCopy();

                return true;

                
            }
        }
        //---------------------------------------------Send Reminder Function General------------------------------------------//
        public bool RemindEmailGeneral(string name, string BreifD, DateTime StartDate, TimeSpan Timespan)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials(SettingService.UserName(), SettingService.PasswordSender(), SettingService.smtpaddress());
            service.Url = new Uri("https://mail.sssprocess.com/EWS/Exchange.asmx");
            EmailMessage emailMessage = new EmailMessage(service);
            emailMessage.Subject = name;
            DateTime time = DateTime.Today.Add(Timespan);
            string displayTime = time.ToString("hh:mm tt");
            emailMessage.Body = "This is a General Reminder email for " + name + " about " + BreifD + " at " + StartDate.Date.ToString("MM/dd/yyyy") + " " + displayTime;
            emailMessage.ToRecipients.Add(SettingService.EmailAdmin());
            emailMessage.SendAndSaveCopy();

            return true;
        }



    }
}
