//using Appointment.Business.Models;
//using Appointment.ViewModel.Models;
//using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace Appointment.Controllers
//{
//    public class HomeController : Controller
//    {
//        public ActionResult Index()
//        {
            
//            return View(ReminderService.Read());
//        }

//        public ActionResult CustomCommand_Read([DataSourceRequest] DataSourceRequest request)
//        {
//            return Json(ReminderService.Read().ToDataSourceResult(request));
//        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Create(RemindersViewModel reminder)
//        {
//            if (ModelState.IsValid)
//            {
//                //The model is valid - insert the reminder and redisplay the grid.
//                //ReminderService.Create(reminder);

//                //GridRouteValues() is an extension method which returns the 
//                //route values defining the grid state - current page, sort expression, filter etc.
//                RouteValueDictionary routeValues = this.GridRouteValues();

//                return RedirectToAction("Index", routeValues);
//            }

//            //The model is invalid - render the current view to show any validation errors
//            return View("Index", ReminderService.Read());
//        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Update(RemindersViewModel reminder)
//        {
//            if (ModelState.IsValid)
//            {
//                //The model is valid - update the reminder and redisplay the grid.
//                ReminderService.Update(reminder);

//                //GridRouteValues() is an extension method which returns the 
//                //route values defining the grid state - current page, sort expression, filter etc.
//                RouteValueDictionary routeValues = this.GridRouteValues();

//                return RedirectToAction("Index", routeValues);
//            }

//            //The model is invalid - render the current view to show any validation errors
//            return View("Index", ReminderService.Read());
//        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Destroy(RemindersViewModel reminder)
//        {
//            RouteValueDictionary routeValues;

//            //Delete the record
//            ReminderService.Delete(reminder);

//            routeValues = this.GridRouteValues();

//            //Redisplay the grid
//            return RedirectToAction("Index", routeValues);
//        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }
//    }
//}