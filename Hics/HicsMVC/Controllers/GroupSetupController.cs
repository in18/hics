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
            //ViewBag.GroupList = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            //List<fn_show_lampgroups_Result> erg = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            UserSession us = (UserSession)Session["UserSession"];

            GroupSetupModel gsm = new GroupSetupModel();            
            gsm.GroupSetupList = HicsBL.DbAccess.GetAllLampGroups(us.name, us.pw);
            
            return View(gsm);
        }

        /// <summary>
        /// Vom erhaltenen Model den Gruppennamen an die BL zur weiteren Bearbeitung weiterschicken. 
        /// </summary>
        /// <param name="gsm"></param>
        /// <returns>Rückkehr zum Index</returns>
        [HttpPost]
        public ActionResult AddGroup(GroupSetupModel gsm)
        {
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
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.removeLampGroup(us.name, us.pw, id);
            return RedirectToAction("index");
        }
    }
}