using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeppelinWebsite.Filters;

namespace ZeppelinWebsite.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [CustomAuthorize(Roles ="Customer")]
        public ActionResult Index()
        {


            return View();
        }
    }
}