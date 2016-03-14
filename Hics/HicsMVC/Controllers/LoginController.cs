using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HicsBL; //eingebunden
using HicsMVC.Models;

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

        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            //DbAccess."Login();"

            //If "User=Admin" dann gehe zum Index Admin
            //else "User=User" dann gehe zum Index User

            //Weiteleitung zum "#"
            //return RedirectToAction("#", "#"); 

            return View();                    
        }

    }
}

////Wenn das Datenmodel valide ist, dann
//            if (this.ModelState.IsValid)
//            {
//    // speichere in Session
//    if (Session["Notizliste"] == null)
//    {
//        Session["Notizliste"] = new List<Notiz>();
//    }

//    var x = Session["Notizliste"] as List<Notiz>;
//    //var y = (List<Notiz>)Session["Notizliste"];

//    x.Add(n);

//    //Erstelle Instanz von Notiz in die DB ablegen
//    using (NotizverwaltungEntities cont = new NotizverwaltungEntities())
//    {
//        Notizen no = new Notizen();
//        no.id = n.Id;
//        no.text = n.Text;
//        no.titel = n.Titel;

//        cont.Notizen.Add(no);

//        cont.SaveChanges();
//    }
//}