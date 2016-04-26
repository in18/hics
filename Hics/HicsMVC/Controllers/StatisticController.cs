using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class StatisticController : Controller
    {
        public ActionResult Index()
        {
            
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Statistik-Liste von BL abrufen.
                List<fn_show_lamp_control_history_Result> userStatisticList = HicsBL.DbAccess.GetLogFileComplete(us.name, us.pw);

                //Temporäre Statistik-Liste initialisieren.
                List<fn_show_lamp_control_history_Result> filteredStatisticList = new List<fn_show_lamp_control_history_Result>();

                //Liste invertiert sortieren.
                for (int i = userStatisticList.Count -1; i >= 0; i--)
                {
                    if (userStatisticList[i].user_name.ToLower() == us.name.ToLower())
                    {
                        filteredStatisticList.Add(userStatisticList[i]);
                    }
                }

                //Den Namen aus der User-Session auslesen und per ViewBag an den View schicken.
                ViewBag.Username = us.name;

                return View(filteredStatisticList);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}