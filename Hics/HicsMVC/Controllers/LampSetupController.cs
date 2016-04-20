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
            UserSession us = (UserSession)Session["UserSession"];

            LampSetupModel lsm = new LampSetupModel();
            lsm.Lamplist = new List<fn_show_lamps_Result>();
            try
            {
                List<fn_show_lamps_Result> slrsort = HicsBL.DbAccess.GetAllLamps(us.name, us.pw);

                for (int i = slrsort.Count - 1; i >= 0; i--)
                {
                    lsm.Lamplist.Add(slrsort[i]);
                }         

                return View(lsm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Vom erhaltenen Model den Lampennamen an die BL zur weiteren Bearbeitung weiterschicken. 
        /// </summary>
        /// <param name="lsm"></param>
        /// <returns>Rückkehr zum Index</returns>
        [HttpPost]
        public ActionResult AddLamp(LampSetupModel lsm)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.addLamp(us.name, us.pw, lsm.Id, lsm.Description);
            return RedirectToAction("index");
        }

        /// <summary>
        /// Vom Anchor-Verweis erhaltene ID zur Löschung eines Gruppen-Datensatzes an die BL weiterleiten, um eine vorhandene Lampe zu löschen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rückkehr zum Index</returns>
        public ActionResult DeleteLamp(int id)
        {
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.deleteLamp(us.name, us.pw, id);
            return RedirectToAction("index");
        }


    }
}