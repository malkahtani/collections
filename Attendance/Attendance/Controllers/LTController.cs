using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;
namespace Attendance.Controllers
{
    public class LTController : Controller
    {
        //
        // GET: /LT/

        Leave_Type_Entities lt = new Leave_Type_Entities();
        [Authorize(Roles = "Leave")]
        public ActionResult Index()
        {
            var leavetype = from le in lt.LeaveTypes select le;
            return View(leavetype.ToList());

        }

        [Authorize(Roles = "Leave")]
        public ActionResult Create()
        {
           

            return View();


        }
        [Authorize(Roles = "Leave")]
        [HttpPost]
        public ActionResult Create(LeaveType le)
        {
            if (ModelState.IsValid)
            {
                lt.AddToLeaveTypes(le);
                lt.SaveChanges();
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
            LeaveType le = lt.LeaveTypes.Single(a => a.LT_ID == id);
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

            LeaveType del = lt.LeaveTypes.Single(a => a.LT_ID == id);
            if (del == null)
            {
                return HttpNotFound();
            }
            lt.DeleteObject(del);
            lt.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
