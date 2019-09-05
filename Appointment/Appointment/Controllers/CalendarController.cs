using Appointment.Business.Models;
using Appointment.ViewModel.Enums;
using Appointment.ViewModel.Models;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Controllers
{
    public class CalendarController : Controller
    {
        

        public ActionResult Index()
        {
            //var type = ReminderService.GetType(id);
            //if (type == LookupService.GetLookupIdByCode((int)Lookups.employee))
            return View(CalendarService.DisplayCurrentReminders());
        }


        //public ActionResult Read([DataSourceRequest]DataSourceRequest request, int? ID)
        //{
        //    if (ID.HasValue)
        //        return Json(GetAllContracts(CustomerID.Value).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    else
        //        return null;
        //}



        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            String TheDate="";
            //DateTime CalenderDate = Convert.ToDateTime(TheDate);
            // int total = 0;
            //var data = new List<Appointment.ViewModel.Models.CalendarViewModel>();
            //GetMissingsTransaction(new MissingsTransactionVM() { FromDate = FromDate, ToDate = ToDate }, page.Value, pageSize.Value, out total);

            {
                if (request.Filters.FirstOrDefault() is CompositeFilterDescriptor)
                {
                    var i = 0;
                    var thisList = (CompositeFilterDescriptor)request.Filters.FirstOrDefault();

                    foreach (FilterDescriptor item in thisList.FilterDescriptors)
                    {
                        //  var thisItem = (item.FilterDescriptors[i] as FilterDescriptor);
                        if (item.Member == "TheDate")
                            TheDate = item.ConvertedValue.ToString();
                    
                        i++;
                    }
                }
                else
                {
                    var thisItem = (request.Filters.FirstOrDefault() as FilterDescriptor);
                 
                    if (thisItem.Member == "TheDate")
                        TheDate = thisItem.ConvertedValue.ToString();                
                }
            }
            DateTime CalenderDate = Convert.ToDateTime(TheDate);
            var data = CalendarService.DisplayFilteredReminders(CalenderDate);
            var result = new DataSourceResult
            {
                Data = data,
                //Total = total
            };
            return Json(result, JsonRequestBehavior.AllowGet);


        }
    }

    
}