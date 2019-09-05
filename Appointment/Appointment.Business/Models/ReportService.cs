using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Web.Mvc;
using Appointment.DAL.Models;
using Appointment.ViewModel.Enums;

namespace Appointment.Business.Models
{
    public class ReportService
    {
        public static string GetCodeForTypeID(int id)
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var Lookups = db.Lookups.Where(x => x.ID == id).FirstOrDefault();
                return Lookups.Code;
            }
        }


        //public static List<SelectListItem> GetYearList()
        //{
        //    var count = DateTime.Now.Year - 1990;
        //    var list = new SelectList(Enumerable.Range( 1990, count + 1).Select(x =>

        //     new SelectListItem()
        //     {
        //         Text = x.ToString(),
        //         Value = x.ToString()
        //     }), "Value", "Text");
        //    return list.ToList();

        //}
        public static List<SelectListItem> GetTypeID()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                var ReminderType = db.LookUpCategories.Where(x => x.Code == ((int)LookupCategories.ReminderType).ToString()).FirstOrDefault().ID;
                var list = db.Lookups.Where(m => m.CategoryID == ReminderType).Select(m => new SelectListItem
                {
                    Value = m.ID.ToString(),
                    Text = m.Description,
                }).ToList();
                return list;
            }
        }

        public static ReportViewer GetparamReport(string ReportName, IEnumerable<Microsoft.Reporting.WebForms.ReportParameter> parameters)
        {
            ReportViewer rptViewer = new ReportViewer();

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportAdminPath"].ToString() + ReportName;
            rptViewer.ZoomMode = ZoomMode.FullPage;

            rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"].ToString());

            rptViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(
                ConfigurationManager.AppSettings["ReportingUserName"].ToString(),
                ConfigurationManager.AppSettings["ReportingPassword"].ToString(),
                ConfigurationManager.AppSettings["ReportingUserDomain"].ToString());
            rptViewer.ServerReport.SetParameters(parameters);
            rptViewer.AsyncRendering = false;
            rptViewer.SizeToReportContent = true;
            return rptViewer;
        }
    }


    public class CustomReportCredentials : IReportServerCredentials
    {
        private string userName;
        private string passWord;
        private string domainName;

        public CustomReportCredentials(string userName, string passWord, string domainName)
        {
            this.userName = userName;
            this.passWord = passWord;
            this.domainName = domainName;
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password,
            out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(userName, passWord, domainName);
            }
        }


    }

}
