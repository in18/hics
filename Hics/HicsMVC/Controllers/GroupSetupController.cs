﻿using System;
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
            GroupSetupModel gsm = new GroupSetupModel();

            List<fn_show_lampgroups_Result> grouplist = new List<fn_show_lampgroups_Result>(); 
            //HicsBL.DbAccess.GetAllLampGroups("Sepp", "123User!");

            gsm.Grouplist = grouplist;

            return View(gsm);
            //return View();
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