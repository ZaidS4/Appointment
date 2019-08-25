using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: TestErrors
        public ActionResult Index()
        {
            throw new Exception("This is a test error!", new HttpException(500, "Internal Sheldon Error!"));
            return View();
        }

        public ActionResult DisplayError(ErrorObject oError)
        {
            return View(oError);
        }
    }
}