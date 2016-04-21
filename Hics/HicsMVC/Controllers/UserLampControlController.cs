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
        // GET: UserLampControl
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