using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;
using System.Diagnostics;

namespace HicsMVC.Controllers
{
    public class LampAssigmentController : Controller
    {
        // GET: LampAssigment
        public ActionResult Index()
        {

            ViewBag.combogroup = new List<SelectListItem> { };
            ViewBag.combolamp = new List<SelectListItem> { };

            return View();
        }

        [HttpPost]
        public ActionResult Assignment(LampAssignmentModel lam)
        {


            return RedirectToAction("index");
        }

        public ActionResult DeleteEntry()
        {


            return RedirectToAction("index");
        }

    }
}