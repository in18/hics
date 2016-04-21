using HicsBL;
using HicsMVC.SampleClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class AdminStatisticController : Controller
    {
        // GET: AdminStatistic
        /// <summary>
        /// Startpunkt für den Statistic-View der Admin-Benutzer, UserSession-Abfrage, Statistikliste von der BL abfragen
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Listen für Ausgabe initalisieren.
                List<fn_show_lamp_control_history_Result> slchr = new List<fn_show_lamp_control_history_Result>();
                List<fn_show_lamp_control_history_Result> blliste = HicsBL.DbAccess.GetLogFileComplete(us.name, us.pw);

                //Liste invertiert sortieren.
                for (int i = blliste.Count -1; i >= 0;  i--)
                {
                    slchr.Add(blliste[i]);
                }

                ViewBag.Sessionname = us.name;

            return View(slchr);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");                
            }

        }
    }
}