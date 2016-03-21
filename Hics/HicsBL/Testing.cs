using huedotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace HicsBL
{
    class Testing
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hallo Welt");
            //DbAccess.addUser("franzi", "123user!", "Pepi", "123");
            //HueAccess.LoadConfig();
            //HueAccess.getWebClient();
            //HueAccess.getLampList();// lamps liste aktuallisieren

            //HelperClass.SetLampState(1, true);
            //HelperClass.SetLampBrightness(2, 254);
            //DbAccess.addUser("admin", "123user!", "Walter", "123user!");
            //Byte[] ha = HelperClass.GetHash("123user!");
            //HelperClass.GetHash(HelperClass.ByteArrayToString(ha));
            List<fn_show_lamp_control_history_Result> l = new List<fn_show_lamp_control_history_Result>();
            l = DbAccess.GetLogFile("admin", "123user!", new DateTime(1990, 1, 1), new DateTime(2016, 3, 22));

            foreach (var item in l)
            {
                Console.WriteLine($"{item.brightness} {item.user_name} {item.lamp_name} {item.date} {item.status}");
            }


            Console.ReadKey();

        }
    }
}
