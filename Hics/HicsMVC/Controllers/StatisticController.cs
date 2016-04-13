using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class StatisticController : Controller
    {


        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["UserSession"];

            List<fn_show_lamp_control_history_Result> userStatisticList = HicsBL.DbAccess.GetLogFileComplete(us.name, us.pw);

            List<fn_show_lamp_control_history_Result> filteredStatisticList = new List<fn_show_lamp_control_history_Result>();

            try
            {

                for (int i = 0; i < userStatisticList.Count; i++)
                {
                    if (userStatisticList[i].user_name == us.name)
                    {
                        filteredStatisticList.Add(userStatisticList[i]);
                    }
                }

                return View(filteredStatisticList);
            }
            catch (Exception)
            {
                return View(new fn_show_lamp_control_history_Result { address = "", lamp_name = "No database connection", user_name = "" });
            }
        }
    }
}