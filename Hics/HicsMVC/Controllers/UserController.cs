using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //eingebunden

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

        [HttpPost]
        public ActionResult ChangePassword(UserChangePasswordModel ucpm)
        {
            //if (ucpm.NewPassword == ucpm.RetypeNewPassword)
            //{
            //    DbAccess."ChangePassword();"

            //    Weiteleitung zum Login
            //    return RedirectToAction("Login", "Login");
            //}
            //else
            //{
            //    ViewBag.errorMsg = "Password does not match";
            //}
            return View(ucpm);           
        }
    }
}