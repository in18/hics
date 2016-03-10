using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //eingebunden
using HicsMVC.Models;

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
            //DbAccess."Login();"

            //If "User=Admin" dann gehe zum Index Admin
            //else "User=User" dann gehe zum Index User

            //Weiteleitung zum "#"
            //return RedirectToAction("#", "#"); 

            return View();                    
        }

    }
}