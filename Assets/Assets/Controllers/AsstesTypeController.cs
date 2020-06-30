
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
        AssetManagementNewEntities3 db = new AssetManagementNewEntities3();


        public ActionResult Index()
        {
            var at = from assetstype in db.AssetTypes select assetstype;
            return View(at.ToList());
        }
        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(AssetType asset)
        {
            if (ModelState.IsValid)
            {
                db.AddToAssetTypes(asset);
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

            var del = db.AssetTypes.Single(a => a.AssetTypeID == id);
            if (del == null)
            {
                return HttpNotFound();
            }
            return View(del);
        } 
        
    
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = "") 
        {

            var del = db.AssetTypes.Single(a => a.AssetTypeID == id);
            if (del == null) 
            { 
                return HttpNotFound(); 
            } 
            db.DeleteObject(del);
            db.SaveChanges();
            return RedirectToAction("Index"); 

            }

        
        public ActionResult Edit(string id)
        {
            var ed = db.AssetTypes.Single(a => a.AssetTypeID == id);

            return View(ed);
        }
        [HttpPost]
        public ActionResult Edit(AssetType ed)
        {
            var ed1 = db.AssetTypes.Single(a => a.AssetTypeID == ed.AssetTypeID);
            try
            {
                UpdateModel(ed1);
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
            var det = db.AssetTypes.Single(a => a.AssetTypeID == id);
            return View(det);
        }
    }

   /* public class ReportSiteCategorySearchViewModel
    {
        public SelectList AssetTypeList { get; set; }
        
    }
    public ActionResult getEquipmentByAssetType()
    {
        ReportSiteAssetTypeSearchViewModel viewModel = new ReportSiteAssetTypeSearchViewModel
        {
            AssetTypeList = new SelectList(AssetTypeService.GetAllAssetsTypes().ToList(), "AssetType"),
            siteList = new SelectList(siteService.GetAllSites().ToList(), "AssetTypeID")
        };

        return View(viewModel);
    }*/
}
