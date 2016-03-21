using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //Verweis hinzugefügt und hier eingebunden

namespace HicsMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult LampControl()
        {
            return View();
        }

        /// <summary>
        /// Methode: [HttpGet]
        /// Methode ruft ChangePassword-View auf
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult ChangePassword(UserChangePasswordModel ucpm)
        //{
        //    altes PW mit der DB abgleichen, wenn gleich->neues PW in DB speichern

        //    if ()
        //    {
        //        //    DbAccess."ChangePassword();"

        //        //    Weiteleitung zum Login
        //        return RedirectToAction("Login", "Login");
        //    }
        //    else
        //    {
        //        ViewBag.errorMsg = "Password does not match";
        //    }
        //    return View(ucpm);
        //}
    }
}