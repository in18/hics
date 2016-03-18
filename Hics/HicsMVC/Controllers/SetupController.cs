using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;

namespace HicsMVC.Controllers
{
    public class SetupController : Controller
    {
        /// <summary>
        /// Setup Administrationsview
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AdminRegistration()
        {
            return View();
        }
        /// <summary>
        /// Postmethode vom AdminRegistration View
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        /// <param name="repeatPass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AdminRegistration(SuperAdminModel sam)
        {
            if (ModelState.IsValid) {

                //Weiterleitung der Daten an die Datenbank
                if (sam.Password == sam.RetypePassword)
                {
                    //DbAccess.addUser("admin", sam.Password, "admin", sam.RetypePassword);
                    //Weiterleitung zum LampSetup
                    return RedirectToAction("LampSetup", "Setup");
                }
                else {
                    ViewBag.errorMsg = "Password does not match";
                }
            }
            
            //ist das gleiche kann auch verwendet werden, mit dem Unterschied Data fungiert wie ein Array
            //ViewData["errMsg"] = "Password does not match!";
            
            return View(sam);
        }

        public ActionResult LampSetup()
        {
            return View();
        }
    }
}