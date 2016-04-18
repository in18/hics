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
        /// <summary>
        /// Startpunkt für den LampAssignmentController-View, UserSession-Abfrage, Lampenliste von der BL abfragen, Model initialisieren und an View senden.
        /// </summary>
        /// <returns>LampAssignmentModel</returns>
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
            lam.lampAssignmentList = new List<fn_show_lamp_control_Result>();

            List<fn_show_lamp_control_Result> slcsort = HicsBL.DbAccess.GetLampControl(us.name, us.pw);

            for (int i = slcsort.Count - 1; i >= 0; i --)
            {
                lam.lampAssignmentList.Add(slcsort[i]);
            }

            //Liste an den View schicken
            return View(lam);
        }

        /// <summary>
        /// Vorhandene Lampe einer vorhandenen Gruppe über das erhaltene Model zuordnen, UserSession-Abfrage.
        /// </summary>
        /// <param name="lam"></param>
        /// <returns>Rückkehr zum Index</returns>
        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLampToGroup(us.name, us.pw, lam.groupname, lam.lamp_id);
            return RedirectToAction("index");
        }

        /// <summary>
        /// Vom Anchor-Verweis erhaltene ID und erhaltenen Gruppenname zur Löschung eines Lampen-Gruppen-Zuordnungsdatensatzes an die BL weiterleiten, um eine Zuordnung aufzuheben.
        /// </summary>
        /// <param name="lamp_id"></param>
        /// <param name="groupname"></param>
        /// <returns>Rückkehr zum Index</returns>
        public ActionResult DeleteEntry(int lamp_id, string groupname)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.removeLampFromGroup(us.name, us.pw, groupname, lamp_id);
            return RedirectToAction("index");
        }
    }
}