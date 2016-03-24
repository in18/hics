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
    public class LampAssigmentController : Controller
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

            List<fn_show_lampgroups_Result> grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            List<fn_show_lamps_Result> lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");
            

            //Viewbags mit Gruppen- und Lampenliste per Viewbag an den View übergeben
            ViewBag.GroupList = grouplist;
            ViewBag.LampList  = lamplist;

            //Liste an den View schicken
            return View(HicsBL.DbAccess.GetLampControl("Sepp", "123user!"));
        }

        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {
            return RedirectToAction("index");
        }

        public ActionResult DeleteEntry(int lamp_id, string groupname)
        {
            HicsBL.DbAccess.removeLampFromGroup("Sepp", "123user!", groupname, lamp_id);
            return RedirectToAction("index");
        }
    }
}