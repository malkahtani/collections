using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    public class ManagersController : Controller
    {
        //
        // GET: /Managers/

        private IManagerRepo _repo;
         
        public ManagersController(): this(new ManagersRepo())
        {
        }
         
        public ManagersController(IManagerRepo repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Index()
        {
            return View(_repo.GetManagers());
        }
        

    }
}
