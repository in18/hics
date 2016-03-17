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

            LampAssignmentModel lam = new LampAssignmentModel();

            List<fn_show_lampgroups_Result> grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            List<fn_show_lamps_Result> lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");

            lam.grouplist = grouplist;
            lam.lamplist = lamplist;

            //ViewBag.combogroup = new List<SelectListItem> { };
            //ViewBag.combolamp = new List<SelectListItem> { };

            return View();
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