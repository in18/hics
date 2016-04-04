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
            bool usercorrect = DbAccess.userLogin(lm.Username, lm.Password);

            //Wenn UserLogin Correct
            if (usercorrect)
            {
                //Erstelle Session mit Username und Password
                UserSession us = new UserSession();
                us.name = lm.Username;
                us.pw = lm.Password;


                //hard-coded:
                //Statt True abfrage an BL mit Username/Password
                //us.admin = true; -->

                //4.4.2016 / LEO:
                //foreach (var item in DbAccess.GetAllUser(lm.Username, lm.Password))
                //{
                //    if (item.name = lm.Username)
                //    {

                //    }
                //}             


                Session["UserSession"] = us;
                //Sessionparameter werden in der allgemeinen webconfig konfiguriert
                //Session.Timeout kann theoretisch auch hier konfiguriert werden, macht aber in der Webconfig mehr Sinn.

                //If "User=Admin" dann gehe zum Index Admin
                if (us.admin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "User");
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
