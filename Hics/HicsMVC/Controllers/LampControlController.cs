using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{


    public class LampControlController : Controller
    {
        /// <summary>
        /// 
        /// 
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
                List<HicsBL.fn_show_lamp_control_Result> lamps;

                //Liste Inhalt zuweisen.
                lamps = HicsBL.DbAccess.GetAllLampsStatus(us.name, us.pw).ToList();
                           
                return View(lamps);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

       // AUSKOMMENTIERT LASSEN ---------- KEINE VERWENDUNG ---------
        //[HttpGet]
        //public ActionResult Index_Lampcontrol()
        //{ 
        //    return View();
        //}
        //------------------------------------------------------------


        /// <summary>
        /// Lampcontrol
        /// </summary>
        /// <param name="id">Lampen ID</param>
        /// <returns>Zurück zum index</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Liste initialisieren.
                List<HicsBL.fn_show_lamp_control_Result> lamp;

                //Liste Inhalt zuwesien.
                lamp = HicsBL.DbAccess.GetAllLampsStatus(us.name, us.pw).ToList();
            
                 HicsBL.fn_show_lamp_control_Result erg = lamp.Where(x => x.lamp_id == id).FirstOrDefault();
                //List<LampControl> lamps = DbHelper.DbHelperClass.getLamps();
                // List<HicsBL.fn_show_lamps_Result> lamps = HicsBL.DbAccess.GetAllLamps("admin","123user!");
                //LampControl lc = (from a in lamps where a.id == id select a).FirstOrDefault<LampControl>();

                return View(erg);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }



        /// <summary>
        /// Lampcontrol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupname"></param>
        /// <param name="lampname"></param>
        /// <param name="status"></param>
        /// <param name="brigthness"></param>
        /// <returns>übernimmt die Daten vom View und speichert diese auf dem Server</returns>
        [HttpPost]
        public ActionResult Edit (HicsBL.fn_show_lamp_control_Result l)
        {
            try
            {
                UserSession us = (UserSession)Session["UserSession"];
                HicsBL.DbAccess.dimLamp(us.name, us.pw, (int)l.lamp_id, (byte)l.brightness,(bool)l.status);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}