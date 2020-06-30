
//AsstesTypeController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assets.Models;

namespace Assets.Controllers
{
    public class AsstesTypeController : Controller
    {
        //
        // GET: /AsstesType/
        AssetsTypeEnt db = new AssetsTypeEnt();


        public ActionResult Index()
        {
            var at = from assetstype in db.Asset_Type select assetstype;
            return View(at.ToList());
        }
        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(Asset_Type asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToAsset_Type(asset);
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
            Asset_Type ast = db.Asset_Type.Single(a => a.Ass_Typ_ID == id);
            if (ast == null)
            {
                return HttpNotFound();
            }
            return View(ast);
        } 
 
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0) 
        {
         
            var del = db.Asset_Type.Single(a => a.Ass_Typ_ID == id);
            if (del == null) 
            { 
                return HttpNotFound(); 
            } 
            db.DeleteObject(del);
            db.SaveChanges();
            return RedirectToAction("Index"); 

            }

        
        public ActionResult Edit(int id)
        {
            var ed = db.Asset_Type.Single(a => a.Ass_Typ_ID == id);

            return View(ed);
        }
        [HttpPost]
        public ActionResult Edit(Asset_Type ed)
        {
            var ed1 = db.Asset_Type.Single(a => a.Ass_Typ_ID == ed.Ass_Typ_ID);
            try
            {
                UpdateModel(ed1);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(ed);
            }
        }
        
        public ActionResult Details(int id)
        {
            var det = db.Asset_Type.Single(a => a.Ass_Typ_ID == id);
            if (det == null)
            {
                return HttpNotFound();
            } 

            return View(det);
        }
    }
}
