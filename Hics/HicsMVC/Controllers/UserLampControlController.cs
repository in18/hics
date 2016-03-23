using HicsMVC.Models;
using HicsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class UserLampControlController : Controller
    {
        // GET: UserLampControl
        public ActionResult Index()
        {
            List<HicsBL.fn_show_lamp_status_Result> lamps;
            string username = "";
            string password = "";
            //Edit by Bernhard  
            //Lamps hat den falschen Listentyp
            //Lamps=List<fn_show_lamps_status_result>
            //GetAllLamps =List<fn_show_lampgroups_result>
            //Ihr benötigt hier -> DbAccess.GetAllLampsStatus()
            //Mit List<fn_show_lamp_control_Result>^^
            //lamps = HicsBL.DbAccess.GetAllLampGroups(username, password);
            //return View(lamps);
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {

            List<LampControl> lamps;
            //Wenn nicht funktionstüchtig bitte auskommentieren
            //lamps = HicsBL.fn_show_lamps_Result.;

            return View();
        }
    }
}