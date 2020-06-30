using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    public class AssetsLocationController : Controller
    {
        //
        // GET: /AssetsLocation/
        AssetManagementNewEntities2 db = new AssetManagementNewEntities2();


        public ActionResult Index()
        {
            var al = from l in db.AssetLocations select l;

            return View(al.ToList());
        }
        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(AssetLocation asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToAssetLocations(asset);
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

            var al = db.AssetLocations.Single(a => a.LocationID == id);
            if (al == null)
            {
                return HttpNotFound();
            }
            return View(al);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = "")
        {

            var al = db.AssetLocations.Single(a => a.LocationID == id);
            if (al == null)
            {
                return HttpNotFound();
            }
            db.DeleteObject(al);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            var al = db.AssetLocations.Single(a => a.LocationID == id);

            return View(al);
        }
        [HttpPost]
        public ActionResult Edit(AssetLocation al)
        {
            var al1 = db.AssetLocations.Single(a => a.LocationID == al.LocationID);
            try
            {
                UpdateModel(al1);
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
            var det = db.AssetLocations.Single(a => a.LocationID == id);
            return View(det);
        }

    }
}
