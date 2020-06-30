using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISE_TEST_V1.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ISE Electronic Exam Centre";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
