using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    
    public class AssetController : Controller
    { 
        //
        // GET: /AssetsLocation/
        AssetManagementNewEntities db = new AssetManagementNewEntities();
        AssetManagementNewEntities2 dbl = new AssetManagementNewEntities2();
        AssetManagementNewEntities3 dbt = new AssetManagementNewEntities3();
        AssetManagementNewEntities5 dbu = new AssetManagementNewEntities5();

        public ActionResult Index()
        {
            var ax = from x in db.Assets select x;
            
            

            return View(ax.ToList());
        }
        public ActionResult Create() {
            var al = from l in dbl.AssetLocations select l;
            var at = from t in dbt.AssetTypes select t;
            var au = from u in dbu.Users select u;
            /*List<AssetType> h = at.ToList();
            h[1].AssetType1*/
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            ViewBag.AssetTypeID = new SelectList(at.AsEnumerable(), "AssetTypeID", "AssetType1", 3);
            ViewBag.EmployeeID = new SelectList(au.AsEnumerable(), "EmployeeID", "OwnerName", 3);
            Asset s = new Asset ();

            s.TimeStamp = DateTime.Now;
            s.TimeStamp.ToString("yyyyMMddHHmmssffff");
            return View("Create", s); 

             }

        [HttpPost]
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToAssets(asset);
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

            var ax = db.Assets.Single(a => a.LabelID == id);
            if (ax == null)
            {
                return HttpNotFound();
            }
            return View(ax);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = "")
        {

            var ax = db.Assets.Single(a => a.LabelID == id);
            if (ax == null)
            {
                return HttpNotFound();
            }
            db.DeleteObject(ax);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            var ax = db.Assets.Single(a => a.LabelID == id);
            var al = from l in dbl.AssetLocations select l;
            var at = from t in dbt.AssetTypes select t;
            var au = from u in dbu.Users select u;
            /*List<AssetType> h = at.ToList();
            h[1].AssetType1*/
            ViewBag.LocationID = new SelectList(al.AsEnumerable(), "LocationID", "Location", 3);
            ViewBag.AssetTypeID = new SelectList(at.AsEnumerable(), "AssetTypeID", "AssetType1", 3);
            ViewBag.EmployeeID = new SelectList(au.AsEnumerable(), "EmployeeID", "OwnerName", 3);
            return View(ax);
        }
        [HttpPost]
        public ActionResult Edit(Asset ax)
        {
            var ax1 = db.Assets.Single(a => a.LabelID == ax.LabelID);
            try
            {
                UpdateModel(ax1);
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
            var det = db.Assets.Single(a => a.LabelID == id);
            return View(det);
        }

    }
}

