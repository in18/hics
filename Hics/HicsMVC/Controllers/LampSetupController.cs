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
            HicsBL.DbAccess test = new DbAccess();

            List<string> namelist = new List<string>();

            return View(namelist);
        }



        [HttpPost]
        public ActionResult AddLamp(LampSetupModel lsm)
        {
                        
            Debug.WriteLine($"ID: {lsm.id}\nDesc: {lsm.description}");            
            

            return RedirectToAction("index");
        }

        public ActionResult DeleteLamp(int id)
        {
            return RedirectToAction("index");
        }


    }
}