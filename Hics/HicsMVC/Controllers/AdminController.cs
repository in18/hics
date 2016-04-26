using HicsMVC.SampleClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsMVC.Models;
using HicsBL;

namespace HicsMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Der Administrator hat hier das Recht das Password aller Benutzer zu ändern,
        /// innerhalb der aktuellen Session
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangePassword(int id)
        {

            //Model initialisieren.
            AdminChangePasswordModel acpm = new AdminChangePasswordModel();

            //Momentane Usersession Abfragen und zur weiteren Benutzung zur Verfügung stellen.
            UserSession us = (UserSession)Session["UserSession"];

            //Session-Name über Viewbag an View übermitteln.
            ViewBag.Adminstatus = us.name.ToLower();

            //Inhalt aus Datenbank auslesen und in Liste speichern 
            List<fn_show_users_Result> users = HicsBL.DbAccess.GetAllUser(us.name, us.pw);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].name.ToLower() == us.name.ToLower())
                {
                    ViewBag.SessionId = users[i].id;
                    break;
                }
            }
            acpm.id = id;

            ViewBag.UserId = id;
            return View(acpm);
        }
        /// <summary>
        /// Zugehörige Post Methode zum ändern des Passwortes
        /// </summary>
        /// <param name="acpm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(AdminChangePasswordModel acpm)
        {            
            UserSession us = (UserSession)Session["UserSession"];


            //Überprüfung des eingegebenen Passworts auf die Übereinstimmung
            if (acpm.NewPassword == acpm.RetypeNewPassword)
            {
                //Unterscheidung zwischen User (Recent-Password == Null [weil bei der User-PW-Zurücksetzung keine Eingabe des aktuellen Passwortes vorhanden ist]) und dem eigenen Admin-Konto (wenn Recent-Password mit Session-Password übereinstimmt)
                if (acpm.RecentPassword == null)
                {
                    DbAccess.ChangePasswordByAdmin(us.name, us.pw, acpm.id, acpm.NewPassword);
                }
                else if (acpm.RecentPassword == us.pw)
                {
                    DbAccess.EditUserPassword(us.name, acpm.RecentPassword, acpm.NewPassword);
                }
            }
            else
            {
                return RedirectToAction("changepassword", "admin");
            }

            return RedirectToAction("index", "useradd");
        }
    }
}