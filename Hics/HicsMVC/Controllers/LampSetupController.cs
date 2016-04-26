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

        /// <summary>
        /// Startpunkt für den LampSetup-View, UserSession-Abfrage, Lampenliste von der BL abfragen, Model initialisieren und an View senden.
        /// </summary>
        /// <returns>LampSetupModel</returns>
        public ActionResult Index()
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Model initialisieren.
                LampSetupModel lsm = new LampSetupModel();

                //Model-Lampenliste initialisieren.
                lsm.Lamplist = new List<fn_show_lamps_Result>();

                //Temporäre Sortierliste Inhalt zuweisen.
                List<fn_show_lamps_Result> slrsort = HicsBL.DbAccess.GetAllLamps(us.name, us.pw);

                //Liste invertieren.
                for (int i = slrsort.Count - 1; i >= 0; i--)
                {
                    lsm.Lamplist.Add(slrsort[i]);
                }         

                return View(lsm);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        /// <summary>
        /// Vom erhaltenen Model den Lampennamen an die BL zur weiteren Bearbeitung weiterschicken. 
        /// </summary>
        /// <param name="lsm">Übergabe ans Model</param>
        /// <returns>Rückkehr zum Index</returns>
        [HttpPost]
        public ActionResult AddLamp(LampSetupModel lsm)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Lampe über BL erstellen.
                HicsBL.DbAccess.addLamp(us.name, us.pw, lsm.Id, lsm.Description);

                //View neu aufbauen.
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        /// <summary>
        /// Vom Anchor-Verweis erhaltene ID zur Löschung eines Gruppen-Datensatzes an die BL weiterleiten, um eine vorhandene Lampe zu löschen.
        /// </summary>
        /// <param name="id">Übergabe Id</param>
        /// <returns>Rückkehr zum Index</returns>
        public ActionResult DeleteLamp(int id)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Lampe über BL als inaktiv markieren.
                HicsBL.DbAccess.deleteLamp(us.name, us.pw, id);

                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}