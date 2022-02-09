using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZeppelinWebsite.Controllers
{
    public class UnAuthorizeController : Controller
    {
        // GET: UnAuthorize
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {

            return View();
        }

    }
}