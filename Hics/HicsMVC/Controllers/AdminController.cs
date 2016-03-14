using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //Verweis hinzugefügt und hier eingebunden

namespace HicsMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Methode: [HttpGet]
        /// Methode ruft ResetPassword-View auf
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(AdminResetPasswordModel arpm)
        {
            if (arpm.NewPassword == arpm.RetypePassword)
            {
            //    DbAccess."ResetPassword();"

            //    Weiteleitung zum Login
            return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.errorMsg = "Password does not match";
            }
            return View(arpm);
        }


    }

}
