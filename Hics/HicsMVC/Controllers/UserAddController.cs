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
            try
            {   
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Model initialisieren.
                UserAddModel uam = new UserAddModel();

                //Model-Liste initialisieren und Liste von BL zuweisen.
                uam.Userlist = HicsBL.DbAccess.GetAllUser(us.name, us.pw);

                //Session-Benutzername an View übergeben.
                ViewBag.Adminname = us.name.ToLower();

                return View(uam);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
        //user erzeugen
        [HttpPost]
        public ActionResult AddAdditionalUser(UserAddModel uam)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Überpfüfungen aus dem Model keine leehren Inhalte
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
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
        //User aus Liste löschen
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                //User-Session-Informationen abrufen.
                UserSession us = (UserSession)Session["UserSession"];

                //Benutzer deaktivierung an BL weiterleiten.
                HicsBL.DbAccess.removeUser(us.name, us.pw, id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}