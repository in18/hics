using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //Verweis hinzugefügt und hier eingebunden
using HicsMVC.Models;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Methode: [HttpGet]
        /// Methode ruft Login-View auf
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            //Schnittstelle 'uer.Login' von BL        
            int usercorrect = DbAccess.userLogin(lm.Username, lm.Password);

            //Wenn UserLogin Correct
            if (usercorrect != 0)
            {
                // 1 = Admin
                // =  Zuweisung
                // == Vergleich
                if (usercorrect == 1)
                {
                    //Erstelle Session mit Username und Password                   
                    UserSession us = new UserSession();
                    us.admin = true;                 
                    us.name = lm.Username;
                    us.pw = lm.Password;

                    //Sessionparameter werden in der allgemeinen Web.config konfiguriert.
                    //Session.Timeout kann theoretisch auch hier konfiguriert werden,
                    // macht aber in der Web.config mehr Sinn.
                    Session["UserSession"] = us;

                    return RedirectToAction("Index", "Admin");
                }
                // 2 = User
                else if (usercorrect == 2)
                {
                    UserSession us = new UserSession();
                    us.admin = false;
                    us.name = lm.Username;
                    us.pw = lm.Password;

                    Session["UserSession"] = us;

                    return RedirectToAction("Index", "User");
                }
                // 3 = nicht vorhanden
                else
                {
                    ViewBag.errorMessage = "Username does not exist";
                    return View();
                }

            }
            ViewBag.errorMessage = "Login failed";
            return View();
        }

    }
}
