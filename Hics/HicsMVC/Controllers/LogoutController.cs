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
        /// Methode ruft Logout-View auf
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return View();            
        }
    }
}