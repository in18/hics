using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class AdminStatisticController : Controller
    {
        // GET: AdminStatistic
        public ActionResult Index()
        {
            return View(HicsBL.DbAccess.GetLogFileComplete("Sepp", "123user!"));
        }
    }
}