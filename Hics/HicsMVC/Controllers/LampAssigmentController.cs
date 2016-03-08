using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class LampAssigmentController : Controller
    {
        // GET: LampAssigment
        public ActionResult Index()
        {

            List<string> combogroup = new List<string>();
            List<string> combolamp = new List<string>();

            ViewBag.combogroup = combogroup;
            ViewBag.combolamp = combolamp;

            return View();
        }
    }
}