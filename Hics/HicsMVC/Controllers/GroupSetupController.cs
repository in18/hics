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
    public class GroupSetupController : Controller
    {
        // GET: GroupSetup
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGroup(GroupSetupModel gsm)
        {
            return RedirectToAction("index");
        }

        public ActionResult DeleteGroup(int id)
        {
            return RedirectToAction("index");
        }

    }
}