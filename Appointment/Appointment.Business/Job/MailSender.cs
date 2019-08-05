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

        //------------------------------------------------Job Execute----------------------------------------------//
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                AppointmentContext db = new AppointmentContext();

                List<DateTime?> BirthDate = new List<DateTime?>();
                BirthDate = db.Reminders.Where(x => x.TypeID == 1).Select(x => x.BirthDate).ToList();

                List<DateTime?> startDate = new List<DateTime?>();
                startDate = db.Reminders.Where(x => x.TypeID == 2).Select(x => x.StartDate).ToList();



                foreach (var item in BirthDate)
                {

                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(StringResource.RemindeBefore));
                    if (t.Day == item.Value.Day && t.Month == item.Value.Month)
                    {
                        bool res = SendEmailBirthday();
                    }

                }

                foreach (var item in startDate)
                {
                    var t = DateTime.Now.Date.AddDays(Convert.ToDouble(StringResource.RemindeBefore));
                    if (t.Day == item.Value.Day && t.Month == item.Value.Month)
                    {
                        bool res = SendEmailGeneral();
                    }

                }



            }
            catch (Exception ex)
            {

            }
        }


        //---------------------------------------------Send Email Function Birthday-----------------------------------------//
        public bool SendEmailBirthday()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int type = Convert.ToInt32(StringResource.typeJob);
            if (type == 1)
            {

                //list used to get all employees Email 
                List<EmployeesViewModel> filter4 = new List<EmployeesViewModel>();
                using (AppointmentContext db2 = new AppointmentContext())
                {
                    var filter1 = db2.Reminders.Where(x => x.TypeID == 1).Select(x => x.ID).ToList();
                    var filter2 = db2.RemindersGroups.Where(x => filter1.Contains(x.ReminderID)).Select(x => x.GroupID).Distinct().ToList();
                    filter4 = db2.EmployeesGroups.Where(s => filter2.Contains(s.GroupID)).Select(s => new EmployeesViewModel { Name = s.Employees.Name, Email = s.Employees.Email }).ToList();
                }

                //used to send the email with  the quartz

                foreach (var item in filter4)
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(StringResource.EmailSender);
                        mail.To.Add(item.Email);
                        mail.Subject = item.Name + " birthday";
                        mail.Body = StringResource.TemplateBirthday;
                        mail.IsBodyHtml = true;


                        //client for email
                        using (SmtpClient smtp = new SmtpClient(StringResource.smtpaddress, Convert.ToInt32(StringResource.portnumber)))
                        {
                            smtp.Credentials = new NetworkCredential(StringResource.EmailSender, StringResource.EmailSenderPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }

                    }

                }
                return true;
            }
            //---------------------------------------------------------------------------------------------------------//
            else if (type == 0)
            {


                //list used to get all employees Email 
                List<EmployeesViewModel> ToAllEmployee = new List<EmployeesViewModel>();
                using (AppointmentContext db1 = new AppointmentContext())
                {
                    ToAllEmployee = db1.Employees.Select(s => new EmployeesViewModel { Name = s.Name, Email = s.Email }).ToList();

                }

                //used to send the email with  the quartz

                foreach (var item in ToAllEmployee)
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(StringResource.EmailSender);
                        mail.To.Add(item.Email);
                        mail.Subject = item.Name + " birthday";
                        mail.Body = StringResource.TemplateBirthday;
                        mail.IsBodyHtml = true;



                        using (SmtpClient smtp = new SmtpClient(StringResource.smtpaddress, Convert.ToInt32(StringResource.portnumber)))
                        {
                            smtp.Credentials = new NetworkCredential(StringResource.EmailSender, StringResource.EmailSenderPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }

                    }

                }
                return true;
            }
            else
                return false;


        }



        //--------------------------------------Send Email Function General-----------------------------------------------//
        public bool SendEmailGeneral()
        {
            List<string> ToAllEmployee = new List<string>();
            using (AppointmentContext db1 = new AppointmentContext())
            {
                ToAllEmployee = db1.Employees.Select(x => x.Email).ToList();
            }

            //used to send the email with  the quartz

            foreach (var item in ToAllEmployee)
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(StringResource.EmailSender);
                    mail.To.Add(item);
                    mail.Subject = "General Reminder";
                    mail.Body = StringResource.TemplateGeneral;
                    mail.IsBodyHtml = true;



                    using (SmtpClient smtp = new SmtpClient(StringResource.smtpaddress, Convert.ToInt32(StringResource.portnumber)))
                    {
                        smtp.Credentials = new NetworkCredential(StringResource.EmailSender, StringResource.EmailSenderPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }

                }

            }
            return true;
        }

    }


}

//-----------------------------------------------Comments Area---------------------------------------------//

//private List<EmployeesViewModel> _employees = AppointmentContext.Employees.Select(m => new EmployeesViewModel { Email = m.Email }).ToList;
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



///////////////////////////////////////////////////////////////////////////////////////////////////////////////