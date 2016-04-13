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

        public ActionResult ChangePassword(int id)
        {
            UserChangePasswordModel ucpm = new UserChangePasswordModel();
            UserSession us = (UserSession)Session["UserSession"];
            ViewBag.Adminstatus = us.name.ToLower();
            List<fn_show_users_Result> users = HicsBL.DbAccess.GetAllUser(us.name, us.pw);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].name == us.name)
                {
                    ViewBag.SessionId = users[i].id;
                    break;
                }
            }

            ViewBag.UserId = id;
            return View(ucpm);
        }
    }
}