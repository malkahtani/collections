using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        EventEntities db = new EventEntities();
         [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Index()
        {
            var c = from m in db.TB_EVENT_LOG select m;

            return View();
        }

    }
}
