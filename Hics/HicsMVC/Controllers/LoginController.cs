using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //[HttpPost]
        //public ActionResult Login(MODELNAME)
        //{
        //    return RedirectToAction("index");
        //} 
               
    }
}