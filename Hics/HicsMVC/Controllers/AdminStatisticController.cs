using HicsBL;
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
            try
            {
            return View(HicsBL.DbAccess.GetLogFileComplete("Sepp", "123user!"));
            }
            catch (Exception)
            {
                return View(new fn_show_lamp_control_history_Result { address = "", lamp_name = "No database connection" });                
            }

        }
    }
}