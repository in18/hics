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
    public class LampAssigmentController : Controller
    {
        // GET: LampAssigment
        public ActionResult Index()
        {
            //Modelerstellung
            LampAssignmentModel lsm = new LampAssignmentModel();

            //Übergabe der BL-Listen an das erstellte Model, Platzhalter für Sessioninfos
            lsm.Grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            lsm.Lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");

            //Model an den View schicken
            return View(lsm);
        }

        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {
            return RedirectToAction("index");
        }

        public ActionResult DeleteEntry()
        {
            return RedirectToAction("index");
        }

    }
}