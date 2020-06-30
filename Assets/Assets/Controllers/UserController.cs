using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        AssetManagementNewEntities5 db = new AssetManagementNewEntities5();

        AssetManagementNewEntities2 dbl = new AssetManagementNewEntities2();

        public ActionResult Index()
        {
            var au = from u in db.Users select u;

            return View(au.ToList());
        }
        public ActionResult Create() {
            var al = from l in dbl.AssetLocations select l;
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            return View(); }

        [HttpPost]
        public ActionResult Create(User asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToUsers(asset);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {

                return View("Create");
            }
        }
        public ActionResult Delete(string id = "")
        {

            var au = db.Users.Single(a => a.EmployeeID == id);
            if (au == null)
            {
                return HttpNotFound();
            }
            return View(au);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = "")
        {

            var au = db.Users.Single(a => a.EmployeeID == id);
            if (au == null)
            {
                return HttpNotFound();
            }
            db.DeleteObject(au);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            var au = db.Users.Single(a => a.EmployeeID == id);
            var al = from l in dbl.AssetLocations select l;
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            return View(au);
        }
        [HttpPost]
        public ActionResult Edit(User au)
        {
            var au1 = db.Users.Single(a => a.EmployeeID == au.EmployeeID);
            try
            {
                UpdateModel(au1);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View("Edit");
            }
        }
        public ActionResult Details(string id)
        {
            var det = db.Users.Single(a => a.EmployeeID == id);
            return View(det);
        }

    }
}
