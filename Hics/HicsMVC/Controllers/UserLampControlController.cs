using HicsMVC.Models;
using HicsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class UserLampControlController : Controller
    {
        
        /// <summary>
        /// Methode: [HttpGet]
        /// Mehtode ruft den UserLampControlController-View auf für den aktuellen User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Liste initialisieren.
                List<fn_show_lamp_control_Result> lamps;

                //Liste Lampenstatus von BL zuweisen.
                lamps = HicsBL.DbAccess.GetAllLampsStatus(us.name, us.pw);

                //UserLampControl ulc = new UserLampControl();
                //ulc.lamplist = HicsBL.DbAccess.GetAllLampsStatus("Sepp", "123user!");

                return View(lamps);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        
        /// <summary>
        /// Mehtode: [HttpGet]
        /// Mehtode ermöglicht das Editieren der ausgewählten Lampen im Edit-View für den
        /// aktuellen User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Liste initialisieren.
                List<fn_show_lamp_control_Result> lamps;

                //Liste Lampenstatus von BL zuweisen.
                lamps = HicsBL.DbAccess.GetAllLampsStatus(us.name, us.pw);


                HicsBL.fn_show_lamp_control_Result erg = lamps.Where(x => x.lamp_id == id).FirstOrDefault();

                return View(erg);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        /// <summary>
        /// Mehtode: [HttpPost]
        /// Methode übergibt die Änderungen an die BL und übernimmt diese
        /// auch im UserLampControlController-View
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(HicsBL.fn_show_lamp_control_Result fn)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Lampenstatus übermitteln
                HicsBL.DbAccess.dimLamp(us.name,us.pw, (int)fn.lamp_id, (byte)fn.brightness, (bool)fn.status);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}