using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;
using System.Diagnostics;

namespace HicsMVC.Controllers
{
    public class LampSetupController : Controller
    {
        // GET: LampSetup
        public ActionResult Index()
        {
            //LampSetupModel lampSetupModel = new LampSetupModel();
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(LampSetupModel lsm)
        {
                        
            Debug.WriteLine($"ID: {lsm.id}\nDesc: {lsm.description}");
            
            

            return RedirectToAction("index");
        }

    }
}