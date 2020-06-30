using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    public class DevicesController : Controller
    {
        //
        // GET: /Devices/
        private IDevicesRepo _repo;
         
        public DevicesController(): this(new DevicesRepo())
        {
        }
         
        public DevicesController(IDevicesRepo repo)
        {
            _repo = repo;
        }
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Index()
        {
            return View(_repo.GetDevices());
        }
        [Authorize(Roles = "Managers,Administrators")]
        public ActionResult Create()
        {
           
            return View();
        }

        [Authorize(Roles = "Managers,Administrators")]
        [HttpPost]
        public ActionResult Create(Devices d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.InsertDevice(d);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View();
        }

    }
}
