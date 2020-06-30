using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;
using System.Threading;
namespace Attendance.Controllers
{
    public class LeaveController : Controller
    {
        //
        // GET: /Leave/
        LeaveEntities l = new LeaveEntities();
        Leave_Type_Entities lt = new Leave_Type_Entities();
        
        [Authorize(Roles = "Leave")]
        public ActionResult Index()
        {
            ViewData["dateFormat"] = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            var leaves = from le in l.Leaves select le;
            return View(leaves.ToList());
            
        }
        [Authorize(Roles = "Leave")]
        public ActionResult Create() {
            //LeaveType mylt = new LeaveType();
            var leavetype = from ltype in lt.LeaveTypes select ltype;
            ViewBag.LeaveType = leavetype.ToList();
       
            return View();
    
            
        }
        [Authorize(Roles = "Leave")]
        [HttpPost]
        public ActionResult Create(Leave le)
        {
            if (ModelState.IsValid)
            {
                l.AddToLeaves(le);
                l.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {

                return View("Create");
            }
        }

        [Authorize(Roles = "Leave")]
        public ActionResult Delete(int id = 0)
        {
            Leave le = l.Leaves.Single(a => a.L_ID == id);
            if (le == null)
            {
                return HttpNotFound();
            }
            return View(le);
        }
        [Authorize(Roles = "Leave")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {

            Leave del = l.Leaves.Single(a => a.L_ID == id);
            if (del == null)
            {
                return HttpNotFound();
            }
            l.DeleteObject(del);
            l.SaveChanges();
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
public string CheckUserName(int input)
{
    UsersEntities users = new UsersEntities();
    string userid = input.ToString();
    var getuser = (from m in users.TB_USER where m.sUserID == userid select m).ToList();


    if (getuser.Count == 1)
    {
        return getuser.FirstOrDefault().sUserName;
    }
    else if (getuser.Count < 1)
    {
        return "There is no employee with this employee ID";
    }
    else
    {
        return "There is more than one employee with this employee ID";
    }
    
}


    }
}
