using Appointment.Business.Models;
using Appointment.ViewModel.Enums;
using Appointment.ViewModel.Models;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Reflection.PortableExecutable;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

//test
namespace Appointment.Controllers
{
    [UserRoleAuthorize(Roles = "Admin")]
    public class ReminderController : BaseController
    {
               /// <summary>
        /// Index method displays data from DB into a grid
        /// </summary>
        /// <returns>the reminders in the DB</returns>
        public ActionResult Index(int? id)
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
            if (type == LookupService.GetLookupIdByCode((int)Lookups.employee))
            {
                EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
                obj = ReminderService.EmployeeRemindersGetByID(id);
                obj.Positions = ReminderService.GetPositions();
                obj.Employees = ReminderService.GetEmployees();

                return View("EmployeeReminderUpdate", obj);
            }
            else
            {
                GeneralRemindersViewModel obj = new GeneralRemindersViewModel();
                obj = ReminderService.generalRemindersGetByID(id);
                obj.Groups = ReminderService.GetGroups();
                return View("GeneralReminderUpdate", obj);
            }
        }



        /// <summary>
        /// method gets the changes of reminder that user made then save it in DB
        /// </summary>
        /// <param name="reminder">gets the data from the model</param>
        /// <returns>all reminders in DB after the change is done</returns>
        [HttpPost]
        public ActionResult EmployeeReminderUpdate(EmployeeRemindersViewModel reminder, HttpPostedFileBase image1)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string imgname = DateTime.Now.ToString("yyyyMMddhhmmss") + image1.FileName;
                    reminder.ModifyOn = DateTime.Now;
                    reminder.ModifyBy = 1;
                    if (image1 != null)
                    {
                       string ImagePathphiscal = AppDomain.CurrentDomain.BaseDirectory + "img\\" + imgname;
                        image1.SaveAs(ImagePathphiscal);
                        reminder.ImagePath = "~/img/" + imgname;
                    }

                    //The model is valid - update the reminder and redisplay the grid.
                    ReminderService.EmployeeReminderUpdate(reminder, image1);

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


                    //The model is valid - update the reminder and redisplay the grid.
                    ReminderService.GeneralReminderUpdate(reminder);

                    //GridRouteValues() is an extension method which returns the 
                    //route values defining the grid state - current page, sort expression, filter etc.

                    return RedirectToAction("Index", ReminderService.Read());
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
        /// method called when new general reminder is clicked
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult NewGeneralReminder()
        {
            GeneralRemindersViewModel obj = new GeneralRemindersViewModel();
            obj.Groups = ReminderService.GetGroups();


            return View(obj);
        }


        /// <summary>
        /// creates a new reminder of type general
        /// </summary>
        /// <param name="reminder">new reminders data</param>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult NewGeneralReminder(GeneralRemindersViewModel reminder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The model is valid - insert the reminder and redisplay the grid.

                    reminder.CreatedOn = DateTime.Now;
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
            catch (Exception ex)
            {

                throw ex;
            }
            
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
            List<string> Errors = new List<string>();

            bool isvalid = false;
            if (ModelState.IsValid)
            {
                
                reminder.CreatedOn = DateTime.Now;
                reminder.CreatedBy = 1;
                ReminderService.Create(reminder);


                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                RouteValueDictionary routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }
            else
            {

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
            if (type == LookupService.GetLookupIdByCode((int)Lookups.employee))
            {
                EmployeeRemindersViewModel obj = new EmployeeRemindersViewModel();
                obj = ReminderService.EmployeeRemindersGetByID(id);
                obj.Positions = ReminderService.GetPositions();
                return View("Details", obj);
            }
            else
            {
                GeneralRemindersViewModel obj = new GeneralRemindersViewModel();
                obj = ReminderService.generalRemindersGetByID(id);
                return View("GeneralDetails", obj);
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
        /// Method to bring the employee email in email textbox
        /// </summary>
        /// <param name="ID">selected employee id</param>
        /// <returns>Email</returns>
        [HttpGet]
        public JsonResult FetchEmail(int ID,int? ReminderID)
        {
            var Email = ReminderService.GetEmail(ID);
            var used = ReminderService.CheckUsedEmployee(ID, ReminderID);
            //return an object
            return Json(new { Email = Email, CanBeUsed = !used }, JsonRequestBehavior.AllowGet);
        }




        
    }
}


