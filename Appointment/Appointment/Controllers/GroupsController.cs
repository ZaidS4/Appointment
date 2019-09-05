using Appointment.Business.Models;
using Appointment.ViewModel.Models;
using Kendo.Mvc.Extensions;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Appointment.Controllers
{
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult Groups()
        {
            return View(GroupService.Read());
        }

        [HttpGet]
        public ActionResult Create()
        {
            //return View(/*GroupService.GetAllEmployee()*/);
            EmployeesGroupsViewModel group = new EmployeesGroupsViewModel();
            group.Employees = GroupService.GetAllEmployee();
            return View(group);

        }

        //[HttpGet]
        //public JsonResult ListCheck(EmployeesGroupsViewModel group)
        //{
        //    group.CreatedOn = DateTime.Now;
        //    group.ModifyOn = DateTime.Now;
        //    group.CreatedBY = 1;
        //    group.ModifyBy = 1;
        //    try
        //    {
        //        bool noselecteditem = GroupService.Noselecteditem(group);

        //        return Json(new { canCreate = !noselecteditem }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        [HttpPost]

        public ActionResult Create(EmployeesGroupsViewModel group)
        {
            if (ModelState.IsValid)
            {
                group.CreatedOn = DateTime.Now;
                group.ModifyOn = DateTime.Now;
                group.CreatedBY = 1;
                group.ModifyBy = 1;

                GroupService.Create(group);


                RouteValueDictionary GrouprouteValues = this.GridRouteValues();
                return RedirectToAction("Groups", GrouprouteValues);
            }

            //The model is invalid - render the current view to show any validation errors
            return View(group);
        }

        // function called by index view when click edit on grid 

        [HttpGet]
        public ActionResult EditInfo(int id)
        {
            EmployeesGroupsViewModel EmpGroup = GroupService.EmployeeGroupsGetByID(id);
            EmpGroup.Employees = GroupService.GetAllEmployee();
            return View(EmpGroup);
        }

        [HttpPost]
        public ActionResult EditInfo(EmployeesGroupsViewModel EmpGroup)
        {

            EmpGroup.CreatedOn = DateTime.Now;
            EmpGroup.ModifyOn = DateTime.Now;
            EmpGroup.CreatedBY = 1;
            EmpGroup.ModifyBy = 1;
            GroupService.EditGroup(EmpGroup);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Groups", routeValues);              
             
        }

        ///////////////////////////
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(GroupService.EmployeeGroupsGetByID(id));
        }


        /////////////////////////
        [HttpGet]
        public ActionResult Delete(int id )
        {
            return View(GroupService.EmployeeGroupsGetByID(id));
        }

        [HttpGet]
        public JsonResult DeleteCheck(int Id)
        {
            try
            {
                bool haveReminder = GroupService.HaveReminders(Id);
              
                return Json(new { canDelete = !haveReminder },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        [HttpPost]
        public ActionResult Delete(EmployeesGroupsViewModel group)
        { 
            GroupService.Delete(group);
            RedirectToAction("Groups");
            return View("Groups", GroupService.Read());

        }
    }
}