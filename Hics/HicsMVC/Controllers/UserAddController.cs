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
            uam.Userlist = HicsBL.DbAccess.GetAllUser(us.name, us.pw);
            ViewBag.Adminname = us.name.ToLower();
            return View(uam);
        }
        //user erzeugen
        [HttpPost]
        public ActionResult AddAdditionalUser(UserAddModel uam)
        {
            UserSession us = (UserSession)Session["UserSession"];

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }            
            if (uam.NewUserPassword == uam.RetypeNewUserPassword)
                {
                //Überprüfung wenn admin wird bool true/false übergeben
                if (uam.IsAdmin)
                {
                    HicsBL.DbAccess.addUser(us.name, us.pw, uam.NewUserName, uam.NewUserPassword,uam.IsAdmin);
                    return RedirectToAction("Index");
                }
                    HicsBL.DbAccess.addUser(us.name, us.pw, uam.NewUserName, uam.NewUserPassword,uam.IsAdmin);
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
            UserSession us = (UserSession)Session["UserSession"];

            HicsBL.DbAccess.removeUser(us.name, us.pw, id);
            return RedirectToAction("Index");
        }
    }
}