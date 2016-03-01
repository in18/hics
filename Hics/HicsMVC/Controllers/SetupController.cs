using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;

namespace HicsMVC.Controllers
{
    public class SetupController : Controller
    {
        /// <summary>
        /// Setup Administrationsview
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminRegistration()
        {
            return View();
        }
        /// <summary>
        /// Postmethode vom AdminRegistration View
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        /// <param name="repeatPass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AdminRegistration(string adminname string pass, string repeatPass)
        {
            //Weiterleitung der Daten an die Datenbank

            //Weiterleitung zum LampSetup
            return RedirectToAction("LampSetup", "Setup");
        }



        public ActionResult LampSetup()
        {
            return View();
        }
    }
}