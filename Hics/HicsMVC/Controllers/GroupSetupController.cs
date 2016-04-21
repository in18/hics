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
        /// <summary>
        /// Startpunkt für den GroupSetup-View, UserSession-Abfrage, Gruppenliste von der BL abfragen, Model initialisieren und an View senden.
        /// </summary>
        /// <returns>GroupSetupModel</returns>
        public ActionResult Index()
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Model initialisieren.
                GroupSetupModel gsm = new GroupSetupModel();
                gsm.GroupSetupList = new List<fn_show_lampgroups_Result>();

                //Liste für Ausgabe initialisieren.
                List<fn_show_lampgroups_Result> slr = HicsBL.DbAccess.GetAllLampGroups(us.name, us.pw);

                //Liste invertiert sortieren.
                for (int i = slr.Count - 1; i >= 0; i--)
                {
                    gsm.GroupSetupList.Add(slr[i]);
                }
            
                return View(gsm);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }

        }

        /// <summary>
        /// Vom erhaltenen Model den Gruppennamen an die BL zur weiteren Bearbeitung weiterschicken. 
        /// </summary>
        /// <param name="gsm"></param>
        /// <returns>Rückkehr zum Index</returns>
        [HttpPost]
        public ActionResult AddGroup(GroupSetupModel gsm)
        {
            //User-Session-Informationen abrufen.
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLampGroup(us.name, us.pw, gsm.Groupname);
            return RedirectToAction("index");
        }

        /// <summary>
        /// Vom Anchor-Verweis erhaltene ID zur Löschung eines Gruppen-Datensatzes an die BL weiterleiten, um eine vorhandene Gruppe zu löschen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rückkehr zum Index</returns>
        public ActionResult DeleteGroup(int id)
        {
            //User-Session-Informationen abrufen.
            UserSession us = (UserSession)Session["UserSession"];

            //angelegte Lampengruppenlöschbefehl an BL per ID weiterleiten.
            HicsBL.DbAccess.removeLampGroup(us.name, us.pw, id);
            return RedirectToAction("index");
        }
    }
}