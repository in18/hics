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
            //List<fn_show_lampgroups_Result> grouplist = HicsBL.DbAccess.GetAllLampGroups("Sepp", "123user!");
            //List<fn_show_lamps_Result> lamplist = HicsBL.DbAccess.GetAllLamps("Sepp", "123user!");

            //Testlisten
            #region Testlisten

            List<fn_show_lampgroups_Result> grouplist = new List<fn_show_lampgroups_Result>();
            List<fn_show_lamps_Result> lamplist = new List<fn_show_lamps_Result>();

            List<SelectListItem> ListItemGroup = new List<SelectListItem>();
            List<SelectListItem> ListItemLamps = new List<SelectListItem>();

            grouplist.Add(new fn_show_lampgroups_Result { id = 1, roomgroupname = "Vorzimmer" });
            grouplist.Add(new fn_show_lampgroups_Result { id = 2, roomgroupname = "Wohnzimmer" });
            grouplist.Add(new fn_show_lampgroups_Result { id = 3, roomgroupname = "Wc" });
            grouplist.Add(new fn_show_lampgroups_Result { id = 4, roomgroupname = "Küche" });
            lamplist.Add(new fn_show_lamps_Result { id = 1, address = "2k91o0", name = "Gang" });
            lamplist.Add(new fn_show_lamps_Result { id = 2, address = "0lk032", name = "Sofa" });
            lamplist.Add(new fn_show_lamps_Result { id = 3, address = "d93k0s", name = "Esstisch" });

            #endregion

            //BL-GruppenListe in SelectListItem-Liste umwandeln
            foreach (var item in grouplist)
            {

                ListItemGroup.Add(new SelectListItem{ Value = item.id.ToString(), Text = item.roomgroupname});
            }

            //BL-LampenListe in SelectListItem-Liste umwandeln
            foreach (var item in lamplist)
            {
                ListItemLamps.Add(new SelectListItem { Value = item.id.ToString(), Text = item.name });
            }

            //Viewbags mit Gruppen- und Lampenliste per Viewbag an den View übergeben
            ViewBag.GroupList = ListItemGroup;
            ViewBag.LampList  = ListItemLamps;

            //Model an den View schicken
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