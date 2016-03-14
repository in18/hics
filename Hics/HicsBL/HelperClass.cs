using huedotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace HicsBL
{
    /// <summary>
    /// In dieser Klasse sind einfache Zugriffsmethoden für HueDotNet
    /// </summary>
    public class HelperClass
    {
        /// <summary>
        /// Methode um einen String zu hashen
        /// </summary>
        /// <param name="text">der text zum hashen</param>
        /// <returns></returns>
        public static Byte[] GetHash(string text)
        {
            Byte[] hashbytes = null;
            SHA512 alg = SHA512.Create();
            Encoding windows1252 = Encoding.GetEncoding(1252);
            //Byte[] result = alg.ComputeHash(Encoding.Unicode.GetBytes(text));
            Byte[] result = windows1252.GetBytes(text);
            hashbytes = alg.ComputeHash(result);
            return hashbytes;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// Steuerung HUE-Bridge
        /// Mit dieser Methode wird eine Lampe real über die HUE-Bridge ein oder ausgeschaltet.
        /// Wichtig die lampId ist die ID der Lampe in der HUE-Bridge. !Nicht in der DB!
        /// </summary>
        /// <param name="lampId">LampenId in der HUE-Bridge</param>
        /// <param name="onOff">on=true off=false</param>
        public static void SetLampState(int lampId, bool onOff)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.state = onOff));
        }

        /// <summary>
        /// Steuerung HUE-Bridge
        /// Mit dieser Methode wird eine Lampe real über die HUE-Bridge eine Helligkeit zugewiesen.
        /// Wichtig die lampId ist die ID der Lampe in der HUE-Bridge. !Nicht in der DB!
        /// </summary>
        /// <param name="lampId">LampenId in der HUE-Bridge</param>
        /// <param name="brightness">Helligkeitswert zwischen 1 und 254</param>
        public static void SetLampBrightness(int lampId, byte brightness)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.brightness = brightness / 255.0));
        }

        /// <summary>
        /// Steuerung HUE-Bridge
        /// Mit dieser Methode wird eine Lampe real über die HUE-Bridge umbenannt.
        /// Wichtig die lampId ist die ID der Lampe in der HUE-Bridge. !Nicht in der DB!
        /// </summary>
        /// <param name="lampId">LampenId in der HUE-Bridge</param>
        /// <param name="lampName">Neuer Name der Lampe</param>
        public static void SetLampName(int lampId, string lampName)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.name = lampName));
        }

        /// <summary>
        /// Ist noch im Aufbau
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="userToken"></param>
        public static void SetXml(string ipAddress, string userToken)
        {
            XElement x = new XElement("settings",
                new XElement("bridgeip", ipAddress),
                new XElement("username", userToken));
        }

        /// <summary>
        /// Die HueLampId wird durch die Abfrage der Db ausgegeben
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="dbLampId">LampenID der DB</param>
        /// <returns></returns>
        public static int GetHueLampId(string username, Byte[] password, int dbLampId)
        {
            int hueLampId = -1;
            //Übergebenes Passwort hashen und in Var pwhash speichern für Übergabe an DB
            //Byte[] pwhash = HelperClass.GetHash(password);
            using (itin18_aktEntities cont = new itin18_aktEntities())
            {
                List<fn_show_lamps_Result> db = cont.fn_show_lamps(username, password).ToList();
                
                foreach (var item in db)
                {
                    if (item.id == dbLampId)
                    {
                        hueLampId = HueAccess.GetLampId(item.name);
                        break;
                    }
                }
            }
            return hueLampId;

        }
        
    }
}
