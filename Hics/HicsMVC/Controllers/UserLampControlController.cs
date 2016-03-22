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
        public ActionResult Index()
        {
            List<HicsBL.fn_show_lamp_status_Result> lamps;
            lamps = HicsBL.DbAccess.GetAllLampGroups.lamps;
            return View(lamps);
        }

        [HttpGet]
        public ActionResult Edit()
        {

            List<LampControl> lamps;
            lamps = HicsBL.fn_show_lamps_Result.;

            return View();
        }
    }
}