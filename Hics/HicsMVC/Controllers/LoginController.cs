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
        /// <summary>
        /// Zugehörige Post Methode für das Login
        /// </summary>
        /// <param name="lm">LoginModel</param>
        /// <returns>Rückkehr zum Hauptmenue</returns>
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
                    us.name = lm.Username.ToLower();
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
                    us.name = lm.Username.ToLower();
                    us.pw = lm.Password;

                    Session["UserSession"] = us;

                    return RedirectToAction("Index", "User");
                }              
            }
            ViewBag.errorMessage = "Login failed";
            return View();
        }

    }
}
