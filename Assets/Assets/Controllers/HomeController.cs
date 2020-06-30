using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = " Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult warranty() { 
            List<SelectListItem> items = new List<SelectListItem>(); 
            items.Add(new SelectListItem { Text = "Action", Value = "0" });
            items.Add(new SelectListItem { Text = "Drama", Value = "1" }); 
            items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });
            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" }); 
            ViewBag.vebdorname = items; return View();
        }
    }
}
