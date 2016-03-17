using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class LogoutController : Controller
    {        
        /// <summary>
        /// Methode: [HttpGet]
        /// Methode löscht die Daten der aktuellen Session und ruft den Login-View auf
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //Daten der aktuellen Session löschen
            Session["UserSession"] = null;

            // Weiteleitung zum Login
            return RedirectToAction("Login", "Login");
        }
        
    }
}