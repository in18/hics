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
    public class GroupSetupController : Controller
    {
        // GET: GroupSetup
        public ActionResult Index()
        {
            //ViewBag.GroupList = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            //List<fn_show_lampgroups_Result> erg = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");

            GroupSetupModel gsm = new GroupSetupModel();            
            gsm.GroupSetupList = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            
            return View(gsm);
        }

        [HttpPost]
        public ActionResult AddGroup(GroupSetupModel gsm)
        {
            return RedirectToAction("index");
        }

        public ActionResult DeleteGroup(int id)
        {
            return RedirectToAction("index");
        }

    }
}