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
    public class LampAssignmentController : Controller
    {
        // GET: LampAssigment
        public ActionResult Index()
        {

            //Momentane Usersession Abfragen und zur weiteren Benutzung zur Verfügung stellen
            UserSession us = (UserSession)Session["UserSession"];

            //User-Session-Advanced mit Fehlerabfrage
            //UserSession us = Session["UserSession"] as UserSession;
            //if (us == null)
            //    //Fehler
            //    Debug.WriteLine("Falscher Datentyp");

            LampAssignmentModel lam = new LampAssignmentModel();

            lam.grouplist = HicsBL.DbAccess.GetAllLampGroups(us.name, us.pw);
            lam.lamplist = HicsBL.DbAccess.GetAllLamps(us.name, us.pw);
            lam.lampAssignmentList = HicsBL.DbAccess.GetLampControl(us.name, us.pw);

            //Liste an den View schicken
            return View(lam);
        }

        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLampToGroup(us.name, us.pw, lam.groupname, lam.lamp_id);
            return RedirectToAction("index");
        }

        public ActionResult DeleteEntry(int lamp_id, string groupname)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.removeLampFromGroup(us.name, us.pw, groupname, lamp_id);
            return RedirectToAction("index");
        }
    }
}