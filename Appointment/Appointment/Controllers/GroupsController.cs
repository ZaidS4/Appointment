using Appointment.Business.Models;
using Appointment.DAL;
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
                

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Groups", routeValues);
            }

            //The model is invalid - render the current view to show any validation errors
            return View(group);
        }

        // function called by index view when click edit on grid 

        [HttpGet]
        public ActionResult Update( )
        {          
            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Update(GroupsViewModel group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The model is valid - update the reminder and redisplay the grid.
                    GroupService.Update(group);

                    //GridRouteValues() is an extension method which returns the 
                    //route values defining the grid state - current page, sort expression, filter etc.
                    RouteValueDictionary routeValues = this.GridRouteValues();

                    return RedirectToAction("Groups", routeValues);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //The model is invalid - render the current view to show any validation errors
            return View("Groups", GroupService.Read());
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Delete(GroupsViewModel group)
        {
            RouteValueDictionary routeValues;

            //Delete the record
            GroupService.Delete(group);

            routeValues = this.GridRouteValues();
            //Redisplay the grid
            RedirectToAction("Groups", routeValues);
            //Redisplay the grid
            return View("Groups", GroupService.Read());

        }


    }
}