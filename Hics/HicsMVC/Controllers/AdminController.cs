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
            //Neues PW mit neuem wiederholtem PW vergleichen, wenn gleich -> neues PW in DB speichern.

            if (arpm.NewPassword == arpm.RetypePassword)
            {
            //    DbAccess."ResetPassword(arpm.#);"

            // Weiteleitung zum Logout
            return RedirectToAction("Logout", "Logout");
            }
            else
            {
                ViewBag.errorMsg = "Password does not match";
            }
            return View(arpm);
        }
    }
}
