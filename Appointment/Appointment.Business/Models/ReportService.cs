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

namespace Appointment.Business.Models
{
    public class ReportService
    {
        public static ReportViewer GetparamReport()
        {
            ReportViewer rptViewer = new ReportViewer();

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(99);
            rptViewer.Height = Unit.Pixel(1000);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"].ToString());

            rptViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(
                ConfigurationManager.AppSettings["ReportingUserName"].ToString(),
                ConfigurationManager.AppSettings["ReportingPassword"].ToString(),
                ConfigurationManager.AppSettings["ReportingUserDomain"].ToString());



            rptViewer.AsyncRendering = false;
            rptViewer.SizeToReportContent = true;
            rptViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportAdminPath"].ToString();
            return rptViewer;
        }
    }

    internal class CustomReportCredentials : IReportServerCredentials
    {
        private string ReportingUserName;
        private string ReportingPassword;
        private string ReportingUserDomain;

        public CustomReportCredentials(string ReportingUserName, string ReportingPassword, string ReportingUserDomain)
        {
            this.ReportingUserName = ReportingUserName;
            this.ReportingPassword = ReportingPassword;
            this.ReportingUserDomain = ReportingUserDomain;
        }

        public WindowsIdentity ImpersonationUser => throw new NotImplementedException();

        public ICredentials NetworkCredentials => throw new NotImplementedException();

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            throw new NotImplementedException();
        }
    }
}
