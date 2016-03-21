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
            LampSetupModel lsm = new LampSetupModel();

            lsm.Lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");            

            return View(lsm);
        }

        [HttpPost]
        public ActionResult AddLamp(LampSetupModel lsm)
        {                        
            return RedirectToAction("index");
        }

        public ActionResult DeleteLamp(int id)
        {
            return RedirectToAction("index");
        }


    }
}