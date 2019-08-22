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

                //Birthday
                List<RemindersViewModel> BirthDate = new List<RemindersViewModel>();
                BirthDate = db.Reminders.Where(x => x.TypeID == 1).Select(x => new RemindersViewModel { Name = x.Employee.Name, BirthDate = x.BirthDate, Email = x.Employee.Email }).ToList();

                //Anniversary
                List<RemindersViewModel> EmployeeStartDate = new List<RemindersViewModel>();
                EmployeeStartDate = db.Reminders.Where(x => x.TypeID == 1).Select(x => new RemindersViewModel { Name = x.Employee.Name, StartDate = x.StartDate, Email = x.Employee.Email }).ToList();

                //general
                List<RemindersViewModel> startDate = new List<RemindersViewModel>();
                startDate = db.Reminders.Where(x => x.TypeID == 2).Select(x => new RemindersViewModel { StartDate = x.StartDate, Name = x.Name, BreifDescription = x.BreifDescription, ID = x.ID }).ToList();


                //For Send Email
                foreach (var item in BirthDate)
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(SettingService.SendBirthday()));
                    if (item.BirthDate.HasValue == true)
                    {
                        if (t.Day == item.BirthDate.Value.Day && t.Month == item.BirthDate.Value.Month)
                        {
                            bool res = SendEmailBirthday(item.Name, item.Email);
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
                            bool res = SendEmailEmployeeStartDate(item.Name, item.Email);
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
                            bool res = SendEmailGeneral(item.Name, item.BreifDescription, item.ID);
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
                            bool res = RemindEmailBirthday(item.Name);
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
                            bool res = RemindEmailEmployeeStartDate(item.Name);
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
                            bool res = RemindEmailGeneral(item.Name, item.BreifDescription);
                        }
                    }

                }


            }
            catch (Exception ex)
            {

            }
        }
        //---------------------------------------------Send Email Function Birthday-----------------------------------------//
        public bool SendEmailBirthday(string name, string email)
        {
            
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SettingService.EmailSender());
                mail.To.Add(email);
                mail.Subject = name + " birthday";
                mail.Body = SettingService.BirthDayEmailText();
                mail.IsBodyHtml = true;


                //client for email
                using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                {
                    smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }

            //}
            return true;
        }
        //---------------------------------------------Send Email Function Anniversary--------------------------------------//
        public bool SendEmailEmployeeStartDate(string name, string email)
        {
            
            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress(SettingService.EmailSender());
                mail.To.Add(email);
                mail.Subject = name + " Anniversary Time !!";
                mail.Body = SettingService.AnniversaryEmailText();
                mail.IsBodyHtml = true;



                using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                {
                    smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }

            //}
            return true;



        }
        //---------------------------------------------Send Email Function General------------------------------------------//
        public bool SendEmailGeneral(string name, string BreifD, int Id)
        {


            List<string> Data = new List<string>();
            using (RemindersEntities db2 = new RemindersEntities())
            {
                var filter1 = db2.Reminders.Where(x => x.ID == Id).Select(x => x.ID).ToList();
                var filter2 = db2.RemindersGroups.Where(x => filter1.Contains(x.ReminderID)).Select(x => x.GroupID).Distinct().ToList();
                Data = db2.EmployeesGroups.Where(s => filter2.Contains(s.GroupID)).Select(s => s.Employee.Email ).ToList();
            }

            foreach (var item in Data)
            {

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(SettingService.EmailSender());
                    mail.To.Add(item);
                    mail.Subject = name + " Reminder";
                    //ask about body for the general Reminder
                    mail.Body = BreifD;
                    mail.IsBodyHtml = true;



                    using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                    {
                        smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }

                }
            }

            return true;
        }

        //---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//

        //---------------------------------------------Send Reminder Function Birthday-----------------------------------------//
        public bool RemindEmailBirthday(string name)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SettingService.EmailSender());
                mail.To.Add(SettingService.EmailAdmin());
                mail.Subject = name + " birthday";
                mail.Body = " this message is a Birthday Reminder";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                {
                    smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return true;
        }
        //---------------------------------------------Send Reminder Function Anniversary--------------------------------------//
        public bool RemindEmailEmployeeStartDate(string name)
        {
            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress(SettingService.EmailSender());
                mail.To.Add(SettingService.EmailAdmin());
                mail.Subject = "Anniversary Reminder for " + name;
                mail.Body = "This Email is a reminder";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                {
                    smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }


            return true;



        }
        //---------------------------------------------Send Reminder Function General------------------------------------------//
        public bool RemindEmailGeneral(string name, string BreifD)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SettingService.EmailSender());
                mail.To.Add(SettingService.EmailAdmin());
                mail.Subject = name + " Reminder";
                //ask about body for the general Reminder
                mail.Body = "This Email is a reminder For " + BreifD ;
                mail.IsBodyHtml = true;



                using (SmtpClient smtp = new SmtpClient(SettingService.smtpaddress(), Convert.ToInt32(SettingService.portnumber())))
                {
                    smtp.Credentials = new NetworkCredential(SettingService.EmailSender(), SettingService.PasswordSender());
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }


            return true;
        }
    }
}
//-----------------------------------------------Comments Area---------------------------------------------//

//private List<EmployeesViewModel> _employees = RemindersEntities.Employees.Select(m => new EmployeesViewModel { Email = m.Email }).ToList;
//foreach (var item in EmployeesViewModel)
//{

//}
//string emailTo = "ahmad728031@gmail.com";
//apR = 
//var m = apR.Read();
//string fileName = Path.GetFileName(img.FileName);
//mail.Attachments.Add(new Attachment(img.InputStream, fileName));
//var dbomy = new dbom();
//dbomy.movieDetails.Attach();

//-------------For SendEmail()---------------//
//apR.ReadEmployeeGroup().Where(e => e.GroupID == 1).Select(s => s.Employee.Email).ToList();
//List<string> to = apR.Read().Select(d => d.Email).ToList();
//List<string> ToEmployeeGroup = apR.ReadEmployeeGroup().Where(e => e.GroupID == 1).Select(e => e.Employee.Email).ToList();
//ToEmployeeGroup = db.EmployeesGroups.Where(e => e.GroupID == 1).Select(m => m.Employee.Email).ToList();

//var comp = DateTime.Compare(DateTime.Now.Date, item.AddDays(-3));
//List<string> ToAllEmployee = apR.Read().Select(d => d.Email).ToList();

//------------------------------//
//db2.Reminders.Where(x=>x.type.Code ==(int) ViewModel.Enums.Lookups.emplyee)  **LOOKUP**
//List<string> ToEmployeeGroup = new List<string>();

//List<int> type = new List<int>();
////send email depend on the typeID
//type = db2.Reminders.Select(x => x.TypeID).ToList();
//foreach (var item in type)
//{
//    if (item == 1)
//    {

//    }
//    else if (item == 2)
//    {

//    }
//}
//var comp = DateTime.Compare(DateTime.Now.Date, item.AddDays(-3));


/////////////////////////////////////////////////////////////////////////////////////////////////////////////
//list used to get all employees Email in specific Group
//List<EmployeesViewModel> filter4 = new List<EmployeesViewModel>();
//using (RemindersEntities db2 = new RemindersEntities())
//{
//    var filter1 = db2.Reminders.Where(x => x.TypeID == 1).Select(x => x.ID).ToList();
//    var filter2 = db2.RemindersGroups.Where(x => filter1.Contains(x.ReminderID)).Select(x => x.GroupID).Distinct().ToList();
//    filter4 = db2.EmployeesGroups.Where(s => filter2.Contains(s.GroupID)).Select(s => new EmployeesViewModel { Name = s.Employee.Name, Email = s.Employee.Email }).ToList();
//}

//used to send the email with  the quartz
//foreach (var item in filter4)
//{

//list used to get all employees Email 
//List<EmployeesViewModel> ToAllEmployee = new List<EmployeesViewModel>();
//using (RemindersEntities db1 = new RemindersEntities())
//{
//    ToAllEmployee = db1.Employees.Select(s => new EmployeesViewModel { Name = s.Name, Email = s.Email }).ToList();

//}

//used to send the email with  the quartz

//foreach (var item in ToAllEmployee)
//{

///////////////////////////////////////////////////////////////////////////////////////////////////////////////