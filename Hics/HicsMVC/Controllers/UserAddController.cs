using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;

namespace HicsMVC.Controllers
{
    public class UserAddController : Controller
    {
        // GET: UserAdd
        public ActionResult Index()
        {
            UserAddModel uam = new UserAddModel();
            List<fn_show_users_Result> userlist = new List<fn_show_users_Result>();
            //HicsBL.DbAccess.GetAllUser("Sepp", "123user!");
            uam.Userlist = userlist;
            return View(uam);
        }
        [HttpPost]
        public ActionResult UserAdd(UserAddModel uam)
        {
            if (uam.NewUserPassword == uam.RetypeNewUserPassword)
            {
                //    DbAccess."ResetPassword();"

                //    Weiteleitung zum Login
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.errorMsg = "Password does not match";
            }
            return View(uam);
        }

        public ActionResult DeleteUser(int id)
        {
            return RedirectToAction("index");
        }
    }
}