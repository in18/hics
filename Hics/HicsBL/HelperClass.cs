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
        public static string GetHash(string text)
        {
            string hash = "";
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
            hash = Encoding.UTF8.GetString(result);
            return hash;
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
        
    }
}
