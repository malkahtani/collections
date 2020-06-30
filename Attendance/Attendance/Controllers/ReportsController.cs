using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        [HttpPost]
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Index(SearchReports Ser)
        {

            List<int> e = new List<int>();
           /* e.Add(530); 
            e.Add(473);
            e.Add(433);
            e.Add(527);
            
            */
            e.AddRange(Ser.id);
           /*
            DateTime t = DateTime.Today;
            DateTime f = new DateTime(2013, 4, 18);
            */
            DateTime t = Ser.t;
            DateTime f = Ser.f;
            IManagerRepo _Mrepo = new ManagersRepo();
            List<Managers> M = _Mrepo.GetManagers().ToList();
            string name = User.Identity.Name;
            string compare = name.ToLower();
            Managers lookup = M.Find(item => item.username == compare);

            Reports R = new Reports(lookup.id, f, t, e); 

            /*var viewModel = new AttendanceView
            {
                viewModel
            };*/
            return View(R.users_r.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Top(Top Ser)
        {

            List<int> e = new List<int>();
            /* e.Add(530); 
             e.Add(473);
             e.Add(433);
             e.Add(527);
            
             */
            e.AddRange(Ser.id);
            /*
             DateTime t = DateTime.Today;
             DateTime f = new DateTime(2013, 4, 18);
             */
            DateTime t = Ser.t;
            DateTime f = Ser.f;
            int number = Ser.num;
            double numb = e.Count/2;
            if (number > Math.Ceiling(numb))
            {
                number = int.Parse(Math.Floor(numb).ToString());
            }
            IManagerRepo _Mrepo = new ManagersRepo();
            List<Managers> M = _Mrepo.GetManagers().ToList();
            string name = User.Identity.Name;
            string compare = name.ToLower();
            Managers lookup = M.Find(item => item.username == compare);

            TReports R = new TReports(lookup.id, f, t, e, number);

            /*var viewModel = new AttendanceView
            {
                viewModel
            };*/
            return View(R.toplist.ToList());
        }

    }
}
