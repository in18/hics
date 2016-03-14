using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{


    public class LampControlController : Controller
    {
        
        // GET: LampControl
        [HttpGet]
        public ActionResult Index()
        {
            List<LampControl> lamps = DbHelper.DbHelperClass.getLamps();
            return View(lamps);
        }



        [HttpGet]
        public ActionResult Index_Lampcontrol()
        {
            

            return View();
        }

        /// <summary>
        /// Lampcontrol
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<LampControl> lamps = DbHelper.DbHelperClass.getLamps();
            LampControl lc = (from a in lamps where a.id == id select a).FirstOrDefault<LampControl>();

            return View(lc);
        }



        /// <summary>
        /// Lampcontrol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupname"></param>
        /// <param name="lampname"></param>
        /// <param name="onOff"></param>
        /// <param name="dimmer"></param>
        /// <returns>übernimmt die Daten vom View und speichert diese auf dem Server</returns>
        [HttpPost]
        public ActionResult Edit (LampControl l)
        {
            HicsBL.DbAccess.dimLamp("Hugo", "lmaa", l.id, l.dimmer);

            return View();

        }
    }
}