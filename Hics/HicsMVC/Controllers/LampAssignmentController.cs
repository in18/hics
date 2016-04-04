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

            //Viewbags mit Gruppen- und Lampenliste per Viewbag an den View übergeben
            //ViewBag.GroupList = grouplist;
            //ViewBag.LampList  = lamplist;
            //ViewBag.LampAssignmentList = lampAssignmentList;

            LampAssignmentModel lam = new LampAssignmentModel();

            lam.grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            lam.lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");
            lam.lampAssignmentList = HicsBL.DbAccess.GetLampControl("Sepp", "123user!");

            //Liste an den View schicken
            return View(lam);
        }

        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {
            //if (lam.groupname == || lam.lamp_id == )
            {

            }
            HicsBL.DbAccess.addLampToGroup("Sepp", "123user!", lam.groupname, lam.lamp_id);
            return RedirectToAction("index");
        }

        public ActionResult DeleteEntry(int lamp_id, string groupname)
        {
            HicsBL.DbAccess.removeLampFromGroup("Sepp", "123user!", groupname, lamp_id);
            return RedirectToAction("index");
        }
    }
}