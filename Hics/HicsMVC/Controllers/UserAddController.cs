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
            return RedirectToAction("index");
        }

        public ActionResult DeleteUser(int id)
        {
            return RedirectToAction("index");
        }
    }
}