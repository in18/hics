using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;
using System.Diagnostics;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class GroupSetupController : Controller
    {
        // GET: GroupSetup
        public ActionResult Index()
        {
            //ViewBag.GroupList = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            //List<fn_show_lampgroups_Result> erg = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            UserSession us = (UserSession)Session["UserSession"];

            GroupSetupModel gsm = new GroupSetupModel();            
            gsm.GroupSetupList = HicsBL.DbAccess.GetAllLampGroups(us.name, us.pw);
            
            return View(gsm);
        }

        [HttpPost]
        public ActionResult AddGroup(GroupSetupModel gsm)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLampGroup(us.name, us.pw, gsm.Groupname);
            return RedirectToAction("index");
        }

        public ActionResult DeleteGroup(int id)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.removeLampGroup(us.name, us.pw, id);
            return RedirectToAction("index");
        }
    }
}