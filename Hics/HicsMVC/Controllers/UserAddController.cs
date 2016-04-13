using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL;
using HicsMVC.Models;
using HicsMVC.SampleClass;

namespace HicsMVC.Controllers
{
    public class UserAddController : Controller
    {
        // GET: UserAdd
        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["UserSession"];
            UserAddModel uam = new UserAddModel();
            uam.Userlist = HicsBL.DbAccess.GetAllUser("Sepp", "123user!");
            ViewBag.Adminstatus = us.name; //.ToLower();
            return View(uam);
        }
        //user erzeugen
        [HttpPost]
        public ActionResult AddAdditionalUser(UserAddModel uam)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }            
            if (uam.NewUserPassword == uam.RetypeNewUserPassword)
                {
                //Überprüfung wenn admin wird bool true/false übergeben
                if (uam.IsAdmin)
                {
                    HicsBL.DbAccess.addUser("Sepp", "123user!", uam.NewUserName, uam.NewUserPassword,uam.IsAdmin);
                    return RedirectToAction("Index");
                }
                    HicsBL.DbAccess.addUser("Sepp", "123user!", uam.NewUserName, uam.NewUserPassword,uam.IsAdmin);
                    //    Weiteleitung zum Login
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errorMsg = "Password does not match";
                }
                return RedirectToAction("Index");

        }
        //User aus Liste löschen
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            HicsBL.DbAccess.removeUser("Sepp", "123user!", id);
            return RedirectToAction("Index");
        }
    }
}