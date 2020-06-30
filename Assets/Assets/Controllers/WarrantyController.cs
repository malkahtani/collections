
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    public class WarrantyController : Controller
    {

        AssetManagementNewEntities6 db = new AssetManagementNewEntities6();



        public ActionResult Index()
        {
            var aw = from w in db.Warranties select w;

            return View(aw.ToList());
        }
        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(Warranty asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToWarranties(asset);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {

                return View("Create");
            }
        }
        public ActionResult Delete(int id = 0)
        {

            var aw = db.Warranties.Single(a => a.WarrantyNumber == id);
            if (aw == null)
            {
                return HttpNotFound();
            }
            return View(aw);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {

            var aw = db.Warranties.Single(a => a.WarrantyNumber == id);
            if (aw == null)
            {
                return HttpNotFound();
            }
            db.DeleteObject(aw);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var aw = db.Warranties.Single(a => a.WarrantyNumber == id);

            return View(aw);
        }
        [HttpPost]
        public ActionResult Edit(Warranty aw)
        {
            var aw1 = db.Warranties.Single(a => a.WarrantyNumber == aw.WarrantyNumber);
            try
            {
                UpdateModel(aw1);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View("Edit");
            }
        }
        public ActionResult Details(int id)
        {
            var det = db.Warranties.Single(a => a.WarrantyNumber == id);
            return View(det);
        }

    }
}
