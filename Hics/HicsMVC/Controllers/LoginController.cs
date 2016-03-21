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
            //Von BL erfragen, ob Login erfolgreich war - bool als return
            //DbAccess."Login();"
            bool usercorrect = false;

            //Wenn UserLogin Correct
            if (usercorrect)
            {
                //Erstelle Session mit Username und Password
                UserSession us = new UserSession();
                //us.ID = 
                us.name = lm.Username;
                us.pw = lm.Password;
                //Statt True abfrage an BL mit Username/Password
                us.admin = true;

                Session["UserSession"] = us;
                //Sessionparameter werden in der allgemeinen webconfig konfiguriert
                //Session.Timeout kann theoretisch auch hier konfiguriert werden, macht aber in der Webconfig mehr Sinn.

                //If "User=Admin" dann gehe zum Index Admin
                if (us.admin)
                {
                    return RedirectToAction("index", "admin");
                }
                else
                {
                    return RedirectToAction("index", "user");
                }
            }
            
            //else "User=User" dann gehe zum Index User
            else
            {
                ViewBag.errorMessage = "Login failed";
                return View();
            }                  
        }
    }
}
