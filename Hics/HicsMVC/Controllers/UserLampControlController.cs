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
        [HttpGet]
        public ActionResult Index()
        {
            List<fn_show_lamp_control_Result> lamps;
            lamps = HicsBL.DbAccess.GetAllLampsStatus("Sepp", "123user!");
             

            //UserLampControl ulc = new UserLampControl();
            //ulc.lamplist = HicsBL.DbAccess.GetAllLampsStatus("Sepp", "123user!");

            return View(lamps);
        }

        

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            List<fn_show_lamp_control_Result> lamps;
            lamps = HicsBL.DbAccess.GetAllLampsStatus("Sepp", "123user!");

            HicsBL.fn_show_lamp_control_Result erg = lamps.Where(x => x.lamp_id == id).FirstOrDefault();

            return View(erg);
        }

        [HttpPost]
        public ActionResult Edit(HicsBL.fn_show_lamp_control_Result fn)
        {
            HicsBL.DbAccess.dimLamp("Sepp", "123user!", (int)fn.lamp_id, (byte)fn.brightness, (bool)fn.status);

            return RedirectToAction("Index");
        }
    }
}