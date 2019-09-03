using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Appointment.Business.Models;
using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using Microsoft.Reporting.WebForms;


namespace Appointment.Controllers
{
    public class ReportsController : Controller
    {

        [HttpGet]
        public ActionResult RemindersDistribution()
        {
            RemindersViewModel obj = new RemindersViewModel();
            obj.Type = ReminderService.GetTypeName();
            return View(obj);
        }

        [HttpGet]
        public ActionResult ReminderReport()
        {
            return View();
        }

        // GET: /Report/
        [HttpPost]
        public ActionResult RemindersDistribution(int type, string year)
        {
            DateTime startDate = Convert.ToDateTime(year + "-01-01");
            int nextYear = int.Parse(year) + 1;
            DateTime endDate = Convert.ToDateTime(nextYear.ToString() + "-01-01");

            using (RemindersEntities db = new RemindersEntities())
            {
                var reminders = db.Reminders.Where(x => x.TypeID == type && x.BirthDate >= startDate && x.BirthDate < endDate);
            }

            //List<ReportParameter> p = new List<ReportParameter>();
            //p.Add(new ReportParameter("P_Type", type.ToString(), false));
            //p.Add(new ReportParameter("P_Year", year.Year.ToString(), false));
            //r = ReportingService("", "", "", false, "reporting", p.AsEnumerable());
            //ViewBag.ReportViewer = r.GetparamReport();

            return View("Index");
        }
        [HttpPost]
        public ActionResult ReminderReport(string title, DateTime startDate, DateTime endDate)
        {
            List<ReportParameter> p = new List<ReportParameter>();
            p.Add(new ReportParameter("P_Title", title.ToString(), false));
            p.Add(new ReportParameter("P_StartDate", startDate.Date.ToString(), false));
            p.Add(new ReportParameter("P_EndDate", endDate.Date.ToString(), false));

            //r = new ReportingService("", "", "", false, "reporting", p.AsEnumerable());
            //ViewBag.ReportViewer = r.GetparamReport();
            return View("Index");
        }



    }



}