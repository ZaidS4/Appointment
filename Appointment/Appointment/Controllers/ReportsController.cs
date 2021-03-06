﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Appointment.Business.Models;
using Appointment.ViewModel.Models;
using Microsoft.Reporting.WebForms;
using static Appointment.Business.Models.ReportService;

namespace Appointment.Controllers
{
    public class ReportsController : Controller
    {

        [HttpGet]
        public ActionResult RemindersDistribution()
        {
            ViewBag.Type = ReportService.GetTypeID();
            return View();
        }

        [HttpGet]
        public ActionResult ReminderReport()
        {
            return View();
        }

        // GET: /Report/
        [HttpPost]
        public ActionResult RemindersDistribution(ReportsViewModel rvm, string yearpicker)
        {

            List<ReportParameter> p = new List<ReportParameter>();

            var b = rvm.SelectedType;
            var type = ReportService.GetCodeForTypeID(b);
            p.Add(new ReportParameter("P_TypeID", type.ToString(), false));


            p.Add(new ReportParameter("P_Year", rvm.year, false));
            ViewBag.ReportViewer = GetparamReport("Reminders Distribution", p);


            return View("Index");


        }
        [HttpPost]
        public ActionResult ReminderReport(ReportsViewModel rvm)
        {

            List<ReportParameter> p = new List<ReportParameter>();
            p.Add(new ReportParameter("P_Name", rvm.Name.ToString(), false));
            p.Add(new ReportParameter("P_StartDate", rvm.StartDate.Date.ToString("MM/dd/yyyy"), false));
            p.Add(new ReportParameter("P_EndDate", rvm.EndDate.Date.ToString("MM/dd/yyyy"), false));
            p.Add(new ReportParameter("P_ID", "", false));

            ViewBag.ReportViewer = GetparamReport("RemindersReport", p);
            return View("Index");

        }

       

    }


}