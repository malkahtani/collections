using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    
    public class AssetHistoryController : Controller
    {
        //
        // GET: /AssetHistory/
        AssetManagementNewEntities1 db = new AssetManagementNewEntities1();
        AssetManagementNewEntities2 dbl = new AssetManagementNewEntities2();
        AssetManagementNewEntities3 dbt = new AssetManagementNewEntities3();
        AssetManagementNewEntities5 dbu = new AssetManagementNewEntities5();



        public ActionResult Index()
        {
            var ah = from h in db.AssetHistories select h;

            return View(ah.ToList());
        }
        public ActionResult Create() {
            var al = from l in dbl.AssetLocations select l;
            var at = from t in dbt.AssetTypes select t;
            var au = from u in dbu.Users select u;
            /*List<AssetType> h = at.ToList();
            h[1].AssetType1*/
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            ViewBag.AssetType = new SelectList(at.AsEnumerable(), "AssetType1", "AssetType1", 3);
            ViewBag.EmployeeID = new SelectList(au.AsEnumerable(), "EmployeeID", "OwnerName", 3);
            AssetHistory s = new AssetHistory();

            s.TimeStamp = DateTime.Now;
            s.TimeStamp.ToString("yyyyMMddHHmmssffff");
            return View("Create",s); 
        }

        [HttpPost]
        public ActionResult Create(AssetHistory asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToAssetHistories(asset);
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

            var ah = db.AssetHistories.Single(a => a.LabelID == id);
            if (ah == null)
            {
                return HttpNotFound();
            }
            return View(ah);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = "")
        {

            var ah = db.AssetHistories.Single(a => a.LabelID == id);
            if (ah == null)
            {
                return HttpNotFound();
            }
            db.DeleteObject(ah);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            var ah = db.AssetHistories.Single(a => a.LabelID == id);
            var al = from l in dbl.AssetLocations select l;
            var at = from t in dbt.AssetTypes select t;
            var au = from u in dbu.Users select u;
            /*List<AssetType> h = at.ToList();
            h[1].AssetType1*/
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            ViewBag.AssetType = new SelectList(at.AsEnumerable(), "AssetType1", "AssetType1", 3);
            ViewBag.EmployeeID = new SelectList(au.AsEnumerable(), "EmployeeID", "OwnerName", 3);
            AssetHistory s = new AssetHistory();
            return View(ah);
        }
        [HttpPost]
        public ActionResult Edit(AssetHistory ah)
        {
            var ah1 = db.AssetHistories.Single(a => a.LabelID == ah.LabelID);
            try
            {
                UpdateModel(ah1);
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
            var det = db.AssetHistories.Single(a => a.LabelID == id);
            return View(det);
        }

    }
}
