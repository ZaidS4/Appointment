using Appointment.Business.Models;
using Appointment.ViewModel.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Appointment.Controllers
{
    public class ReminderController : Controller
    {
        /// <summary>
        /// Index method displays data from DB into a grid
        /// </summary>
        /// <returns>the reminders in the DB</returns>
        public ActionResult Index()
        {

            return View(ReminderService.Read());
        }



        
       
        /// <summary>
        /// function called by index view when click edit on grid 
        /// </summary>
        /// <param name="id"> reminder id </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(int id)
        {
            
            var type = ReminderService.GetType(id);
            if (type == 1)
            {
                EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
                obj =  ReminderService.EmployeeRemindersGetByID(id);
                obj.Positions = ReminderService.GetPositions();
                obj.Employees = ReminderService.GetEmployees();

                return View("EmployeeReminderUpdate", obj);
            }
            else
            {
                GeneralRemindersViewModel obj = new GeneralRemindersViewModel();
                obj = ReminderService.generalRemindersGetByID(id);
                return View("GeneralReminderUpdate", obj);
            }
        }


        /// <summary>
        /// method gets the changes of reminder that user made then save it in DB
        /// </summary>
        /// <param name="reminder">gets the data from the model</param>
        /// <returns>all reminders in DB after the change is done</returns>
        [HttpPost]
        public ActionResult EmployeeReminderUpdate(EmployeeRemindersViewModel reminder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    reminder.ModifyOn = DateTime.Now;
                    reminder.ModifyBy = 1;
                    reminder.TypeID = 1;
        
                    //The model is valid - update the reminder and redisplay the grid.
                    ReminderService.EmployeeReminderUpdate(reminder);

                    //GridRouteValues() is an extension method which returns the 
                    //route values defining the grid state - current page, sort expression, filter etc.
                    RouteValueDictionary routeValues = this.GridRouteValues();

                    return View("Index", ReminderService.Read());
                }
                else
                {
                    List<string> Errors = new List<string>();

                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            Errors.Add(error.ErrorMessage);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //The model is invalid - render the current view to show any validation errors
            return View();
        }


        /// <summary>
        /// general reminder edit method
        /// </summary>
        /// <param name="reminder">gets the changes of the reminder to save it</param>
        /// <returns>index view</returns>
        public ActionResult GeneralReminderUpdate(GeneralRemindersViewModel reminder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reminder.ModifyOn = DateTime.Now;
                    reminder.ModifyBy = 1;
                    reminder.TypeID = 2;

                    //The model is valid - update the reminder and redisplay the grid.
                    ReminderService.GeneralReminderUpdate(reminder);

                    //GridRouteValues() is an extension method which returns the 
                    //route values defining the grid state - current page, sort expression, filter etc.

                    return RedirectToAction("Index", ReminderService.Read());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //The model is invalid - render the current view to show any validation errors
            return View();
        }

        /// <summary>
        /// reminder remove method
        /// </summary>
        /// <param name="reminder">the reminder data that selected to be deleted</param>
        /// <returns>index view</returns>
        [HttpPost]
        public ActionResult Delete(RemindersViewModel reminder)
        {
            //RouteValueDictionary routeValues;

            //Delete the record
            ReminderService.Delete(reminder);

            //routeValues = this.GridRouteValues();

            //Redisplay the grid
            RedirectToAction("Index");


            return View("Index", ReminderService.Read());
        }



       
        /// <summary>
        /// method called when new general reminder is clicked
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult NewGeneralReminder()
        {
            return View();
        }

        /// <summary>
        /// creates a new reminder of type general
        /// </summary>
        /// <param name="reminder">new reminders data</param>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult NewGeneralReminder(GeneralRemindersViewModel reminder)
        {
            if (ModelState.IsValid)
            {
                //The model is valid - insert the reminder and redisplay the grid.
                
                reminder.CreatedOn = DateTime.Now;
                reminder.ModifyOn = DateTime.Now;
                reminder.ModifyBy = 1;
                reminder.CreatedBy = 1;

                ReminderService.Create(reminder);

              //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                RouteValueDictionary routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }

            //The model is invalid - render the current view to show any validation errors
            return View();
        }

        /// <summary>
        /// method called when new employee reminder button is clicked
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult NewEmployeeReminder()
        {
            TempData["isvalid"] = true;
            EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
            obj.Positions = ReminderService.GetPositions();
            obj.Employees = ReminderService.GetEmployees();

            return View(obj);
        }


        /// <summary>
        /// creates new reminder of type employee
        /// </summary>
        /// <param name="reminder">new reminders data</param>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult NewEmployeeReminder(EmployeeRemindersViewModel reminder)

        {
            bool isvalid = false;
            if (ModelState.IsValid)
            {
                //The model is valid - insert the reminder and redisplay the grid.

                reminder.CreatedOn = DateTime.Now;
                reminder.ModifyOn = DateTime.Now;
                reminder.ModifyBy = 1;
                reminder.CreatedBy = 1;
                ReminderService.Create(reminder);


                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                RouteValueDictionary routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }
            else
            {
                List<string> Errors = new List<string>();

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage);
                    }
                }
            }
            TempData["isvalid"] = isvalid;
            
            return View();
        }

        /// <summary>
        /// show reminder details of type employee 
        /// </summary>
        /// <param name="id">selected reminder id</param>
        /// <returns>method employeeremindergetbyid call</returns>
        public ViewResult Details(int id)
        {
            var type = ReminderService.GetType(id);
            if (type == 1)
            {
                //EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
                //obj.Positions = ReminderService.GetPositions();
                return View("Details", ReminderService.EmployeeRemindersGetByID(id));
            }
            else
            {
                return View("GeneralDetails", ReminderService.generalRemindersGetByID(id));
            }

            
        }



        /// <summary>
        /// show reminder details of type general 
        /// </summary>
        /// <param name="id">selected reminder id</param>
        /// <returns>method generalremindergetbyid call</returns>
        public ViewResult GeneralDetails(int id)
        {

            
            return View(ReminderService.generalRemindersGetByID(id));
        }

        /// <summary>
        /// method to redirect to contact view
        /// </summary>
        /// <returns>view</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}


