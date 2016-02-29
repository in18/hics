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
            bool isAdmin = false;
            ViewData["isAdmin"] = isAdmin;

            List<SampleClass.LampClass> lcList = new List<SampleClass.LampClass>();
            SampleClass.LampClass lc;

            lc = new SampleClass.LampClass();
            lc.id = 1;
            lc.groupname = "Wohnzimmer";
            lc.lampname = "WZ1";
            lcList.Add(lc);

            lc = new SampleClass.LampClass();
            lc.id = 33;
            lc.groupname = "Wohnzimmer";
            lc.lampname = "WZ2";
            lcList.Add(lc);

            lc = new SampleClass.LampClass();
            lc.id = 156;
            lc.groupname = "Wohnzimmer";
            lc.lampname = "WZ3";
            lcList.Add(lc);
            
            return View(lcList);
        }

        public ActionResult LampOn(int id) {

            // irgendwelche db operationen

            return RedirectToAction("index");
        }

        public ActionResult LampÓff(int id)
        {

            // irgendwelche db operationen

            return RedirectToAction("index");
        }
    }
}