using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HicsMVC.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult Index()
        {
            List<SampleClass.StaticClass> stListe = new List<SampleClass.StaticClass>();
            SampleClass.StaticClass st1 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st2 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st3 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st4 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st5 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st6 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st7 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st8 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st9 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st10 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st11 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st12 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st13 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st14 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            SampleClass.StaticClass st15 = new SampleClass.StaticClass(1, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Wohnzimmer", true);
            SampleClass.StaticClass st16 = new SampleClass.StaticClass(2, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "Vorzimmer", false);
            stListe.Add(st1);
            stListe.Add(st2);
            stListe.Add(st3);
            stListe.Add(st4);
            stListe.Add(st5);
            stListe.Add(st6);
            stListe.Add(st7);
            stListe.Add(st8);
            stListe.Add(st9);
            stListe.Add(st10);
            stListe.Add(st11);
            stListe.Add(st12);
            stListe.Add(st13);
            stListe.Add(st14);
            stListe.Add(st15);
            stListe.Add(st16);

            return View(stListe);
        }
    }
}