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
    public class LampSetupController : Controller
    {
        // GET: LampSetup
        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["UserSession"];

            LampSetupModel lsm = new LampSetupModel();

            lsm.Lamplist = HicsBL.DbAccess.GetAllLamps(us.name, us.pw);            

            return View(lsm);
        }

        [HttpPost]
        public ActionResult AddLamp(LampSetupModel lsm)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLamp(us.name, us.pw, lsm.Id, lsm.Description);
            return RedirectToAction("index");
        }

        public ActionResult DeleteLamp(int id)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.deleteLamp(us.name, us.pw, id);
            return RedirectToAction("index");
        }


    }
}