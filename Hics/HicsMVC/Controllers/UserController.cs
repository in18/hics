using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //[HttpPost]
        //public ActionResult ChangePassword(MODELNAME)
        //{
        //    return RedirectToAction("index");
        //}  
           
    }
}