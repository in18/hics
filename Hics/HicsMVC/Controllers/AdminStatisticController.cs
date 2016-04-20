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

                List<fn_show_lamp_control_history_Result> slchr = new List<fn_show_lamp_control_history_Result>();
                List<fn_show_lamp_control_history_Result> blliste = HicsBL.DbAccess.GetLogFileComplete("Sepp", "123user!");
                for (int i = blliste.Count -1; i >= 0;  i--)
                {
                    slchr.Add(blliste[i]);
                }

            return View(slchr);
            }
            catch (Exception)
            {
                return View(new fn_show_lamp_control_history_Result { address = "", lamp_name = "No database connection" });                
            }

        }
    }
}