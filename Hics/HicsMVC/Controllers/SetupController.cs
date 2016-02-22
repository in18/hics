using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class SetupController : Controller
    {
        /// <summary>
        /// Setup Administrationsview
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminRegistration()
        {
            return View();
        }
    }
}