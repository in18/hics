using System.Web.Helpers;
using System.Web.Mvc;
using System.IO;
using System.Web;


namespace HicsMVC.Controllers
{
    public class HistoryController : Controller
    {


        //private object SeriesChartType;

        //public object Series { get; set; }

        //public int name { get; private set; }

        // GET: History
        public ActionResult Index()
        {
            //Chart chart = new Chart(320, 200);
            //chart.

            // Titel festlegen
            //chart.AddTitle("Mein Schaubild");

            // string chartType;

            // Daten für Schaubild erzeugen

            //chart.AddSeries(string name=0, string chartType="Column", string chartArea=null, string axisLabel=null, string legend=null, int markerStep=1, System.Collections.IEnumerable xValue=null, string xField=null, System.Collections.IEnumerable yValues=null, string yFields=null);
            //chart.AddSeries("Daten1");
            //chart.DataBindTable("Daten1").SetYAxis(3.ToString());
            //chart.DataBindTable("Daten1").SetYAxis(9.ToString());
            //chart.DataBindTable("Daten1").SetYAxis(5.ToString());
            //chart.DataBindTable("Daten1").SetYAxis(7.ToString());

            //chart.Series["Daten1"].Points.AddY(3);
            //chart.Series["Daten1"].Points.AddY(9);
            //chart.Series["Daten1"].Points.AddY(5);
            //chart.Series["Daten1"].Points.AddY(7);


            //object SeriesChartType = null;
            //object Series = null;

            // Typ festlegen
            //chart.Series["Daten1"].ChartType = SeriesChartType.Spline;

            // Zeichenbereich anlegen
            //chart.ChartAreas.Add(new ChartArea());

            // Datenstrom erzeugen und Bild in Datenstrom laden
            //MemoryStream ms = new MemoryStream();
            //chart.Save(ms.ToString());
            //Datenstrom als Bild zurückgeben
            //    return File(ms.GetBuffer(), @"image/png");
            return View();
        }
    }
}