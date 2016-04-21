using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //Verweis hinzugefügt und hier eingebunden
using HicsMVC.SampleClass;

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
            try
            {
                //Altes PW mit der DB abgleichen, wenn gleich -> neues PW in DB speichern.
                if (ucpm.NewPassword == ucpm.RetypeNewPassword)
                {
                    //User-Session-Informationen abrufen.
                    UserSession userdaten = (UserSession)Session["UserSession"];

                    bool b = DbAccess.EditUserPassword(userdaten.name, ucpm.RecentPassword, ucpm.NewPassword);

                    if (b == true)
                    { 
                        return RedirectToAction("Logout", "Logout");
                    }
                    else
                    {
                        ViewBag.errorMessage = "RecentPassword wrong";
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Password does not match";
                }
                return View(ucpm);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }    
    }
}