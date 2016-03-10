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




        //public ActionResult LampOn(int id)
        //{

        //    irgendwelche db operationen

        //    return RedirectToAction("index");
        //}

        //public ActionResult LampOff(int id)
        //{

        //    irgendwelche db operationen

        //    return RedirectToAction("index");
        //}
    }
}