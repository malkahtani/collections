using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;
using System.Threading;
using Telerik.Web.Mvc;
namespace Attendance.Controllers
{
    public class HomeController : Controller
    {
        IManagerRepo _Mrepo;
        List<Managers> M ;
        UsersEntities Users = new UsersEntities();
        List<string> sl = new List<string>();
        public HomeController(){
            _Mrepo = new ManagersRepo();
             M = _Mrepo.GetManagers().ToList();
    }
        private Boolean isManager(int id){
            
           // Managers test = M.Find(item => item.id == id);

            return (M.Exists(item => item.id == id));
        }
        private List<Attend> GetReport(int id)
        {
           // int user = id;
            List<Attend> users = new List<Attend>();
            List<Managers> reports = M.FindAll(item => item.reportto == id).ToList();
            for (int j = 0; j < reports.Count; j++)
            {
                int user11 = reports.ElementAt(j).id;
                
                string userid11 = Convert.ToString(user11).ToUpperInvariant();
                var us11 = (from u in Users.TB_USER where u.sUserID == userid11 select u).ToList();
                string name11 = "-----" + us11.FirstOrDefault().sUserName + "";
                sl.Add(name11);
                users.Add(new Attend() { name = name11, id = user11 });

                for (int i = 0; i < reports.ElementAt(j).employees.Count; i++)
                {
                    int user1 = reports.ElementAt(j).employees.ElementAt(i);
                    string userid = Convert.ToString(user1).ToUpperInvariant();
                    var us = (from u in Users.TB_USER where u.sUserID == userid select u).ToList();
                    string name1 = us.FirstOrDefault().sUserName;
                    sl.Add(name1);
                    users.Add(new Attend() { name = name1, id = user1 });
                }
                if (isManager(user11))
                {
                    if (M.Exists(item => item.reportto == id) && user11 != id)
                    {

                        users.AddRange(GetReport(user11));
                    }
                }
            }
            return users;
        }
        //[Authorize(Roles = "Managers,Administrators")]
        private List<Attend> GetUsers()

        {
            
            List<Attend> users = new List<Attend>();
            string name = User.Identity.Name;
                string compare = name.ToLower();
                Managers lookup = M.Find(item => item.username == compare);
           // List<Managers> report = M.FindAll(item => item.reportto == lookup.id).ToList();
           
            string firstuserid = Convert.ToString(lookup.id).ToUpperInvariant();
            var firstus = (from u in Users.TB_USER where u.sUserID == firstuserid select u).ToList();
            users.Add(new Attend() { name = firstus.ElementAt(0).sUserName, id = lookup.id });
            for (int i = 0; i < lookup.employees.Count; i++)
            {
                int user1 = lookup.employees.ElementAt(i);
                string userid = Convert.ToString(user1).ToUpperInvariant();
                var us = (from u in Users.TB_USER where u.sUserID == userid  select u).ToList();
                string name1 = us.FirstOrDefault().sUserName;
                sl.Add(name1);
                users.Add(new Attend() { name = name1, id = user1 });
            }
            
            users.AddRange(GetReport(lookup.id));
           /* for (int j = 0; j < report.Count; j++)
            {
                int user11 = report.ElementAt(j).id;
                if (isManager(user11))
                {
                    users.AddRange(GetReport(user11));
                }
                string userid11 = Convert.ToString(user11).ToUpperInvariant();
                var us11 = (from u in Users.TB_USER where u.sUserID == userid11 select u).ToList();
                string name11 = "-----" + us11.FirstOrDefault().sUserName + "----";
                sl.Add(name11);
                users.Add(new Attend() { name = name11, id = user11 });

                for (int i = 0; i < report.ElementAt(j).employees.Count; i++)
                {
                    int user1 = report.ElementAt(j).employees.ElementAt(i);
                    string userid = Convert.ToString(user1).ToUpperInvariant();
                    var us = (from u in Users.TB_USER where u.sUserID == userid select u).ToList();
                    string name1 = us.FirstOrDefault().sUserName;
                    users.Add(new Attend() { name = name1, id = user1 });
                }
            }*/
            

            

            

 

            return users;

        }

        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Index()
        {
            SearchReports ser = new SearchReports();

                                   
            ViewData["dateFormat"] = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            ViewBag.Message = "";
           // List<Attend> users = new List<Attend>();
           // users.Add(new Attend() { name = "Mohammad", id = 137 });
            ViewBag.Users = GetUsers();
          //  ViewBag.Users = users;
            return View(ser);
        }
       [Authorize(Roles = "Managers,Administrators")]
        public ActionResult About()
        {
            ViewBag.Message = "Attendance Application";

            return View();
        }
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Contact()
        {
            ViewBag.Message = "m.alkahtani@ise.sa";

            return View();
        }

        
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Top()
        {
            Top ser = new Top();


            ViewData["dateFormat"] = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            ViewBag.Message = "";
            // List<Attend> users = new List<Attend>();
            // users.Add(new Attend() { name = "Mohammad", id = 137 });
            ViewBag.Users = GetUsers();
            //  ViewBag.Users = users;
            return View(ser);
        }
    }
}
