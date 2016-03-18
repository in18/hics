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
            LampAssignmentModel lam = new LampAssignmentModel();

            //List<fn_show_lampgroups_Result> grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            //List<fn_show_lamps_Result> lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");

            //Testlisten
            #region Testlisten

            List<fn_show_lampgroups_Result> grouplist = new List<fn_show_lampgroups_Result>();
            List <fn_show_lamps_Result> lamplist = new List<fn_show_lamps_Result>();

            grouplist.Add(new fn_show_lampgroups_Result { id = 1, roomgroupname = "Vorzimmer" });
            grouplist.Add(new fn_show_lampgroups_Result { id = 2, roomgroupname = "Wohnzimmer" });

            List<SelectListItem> lamplist2 = new List<SelectListItem>();
            SelectListItem singleListItem = null;

            singleListItem = new SelectListItem();
            singleListItem.Text = "Gang";
            singleListItem.Value = "0";
            lamplist2.Add(singleListItem);

            lamplist.Add(new fn_show_lamps_Result { address = "123xyz", name = "Gang" });
            lamplist.Add(new fn_show_lamps_Result { address = "2x48ji", name = "Sofa" });
            lamplist.Add(new fn_show_lamps_Result { address = "j9052k", name = "Esstisch" });

            #endregion

            lam.grouplist = grouplist;
            lam.lamplist = lamplist;

            return View(lam);
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