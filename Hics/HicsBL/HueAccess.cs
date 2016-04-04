using huedotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using NDesk.Options;
using System.Threading;
using System.Xml.Linq;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace HicsBL
{
    /// <summary>
    /// Diese Klasse bietet Methoden zur Steuerung der Hue-Bridge
    /// </summary>
    public class HueAccess
    {
        #region Ja Nichts verändern
        private static String bridgeIP;
        private static String username;
        private static HueMessaging messaging;
        private static Dictionary<int, HueLamp> lamps;

        /// <summary>
        /// Es wird eine aktuelle Auflistung der vorhandenen Lampen in der HUE-Bridge in die Liste "lamps" übertragen
        /// </summary>
        public static void getLampList()
        {
            JsonLampList lampList = JsonConvert.DeserializeObject<JsonLampList>(messaging.DownloadState());
            lamps = lampList.ConvertToHueLamps();
        }

        /// <summary>
        /// Diese Methode erzeugt eine Instanz von HueMessaging mit der IP
        ///  und dem USer bzw Appkey
        /// </summary>
        public static void getWebClient()
        {
            messaging = new HueMessaging(bridgeIP, username);
        }

        /// <summary>
        /// Gibt den in der HUE-Bridege eingetragenen Wert der Helligkeit einer Lampe zurück
        /// </summary>
        /// <param name="lampNumber">LampenId der Hue-Bridge NICHT der Db</param>
        /// <returns>Eingetragener Helligkeitswert der Lampe in der HUE-Bridge</returns>
        public static double GetCurrentLampBrightness(int lampNumber)
        {
            HueLamp lamp;
            lamps.TryGetValue(lampNumber, out lamp);

            if (lamp == null)
            {
                return 254;
            }
            else
            {
                return Math.Round(lamp.brightness * 255);
            }
        }

        /// <summary>
        /// Einen Namen einer Lampe in der aktuellen HUE-Bridge ausgeben zu lassen
        /// </summary>
        /// <param name="lampNumber">HUE-Bridge LampenId</param>
        /// <returns>Eingetragener Lampenname in der HUE-Bridge</returns>
        public static string GetLampName(int lampNumber)
        {
            HueLamp lamp;
            lamps.TryGetValue(lampNumber, out lamp);
            return lamp.name;
        }

        /// <summary>
        /// Eine LampenId aus der aktuellen HUE-Bridge anhand des Lampennamens zu bekommen
        /// </summary>
        /// <param name="lampName">HUE-Bridge Lampenname</param>
        /// <returns>Eingetragene LampenId in der HUE-Bridge</returns>
        public static int GetLampId(string lampName)
        {
            
            int lId = 0;
            LoadConfig();
            getWebClient();
            getLampList();

            //for (int i = 0; i < lamps.Count; i++)
            //{
            //    if (lamps[i].name  == lampName)
            //    {
            //        lId = lamps[i].GetLampNumber();
            //    }
            //}

            foreach (var i in lamps)
            {
                if (i.Value.name == lampName)
                {
                    lId = i.Value.GetLampNumber();
                }
            }
            return lId;
        }

        /// <summary>
        /// Mit dieser Methode können einzelne Werte einer HUE-Lampe gesetzt werden
        /// .brightness die Helligkeit
        /// Der HSV-Farbraum (https://de.wikipedia.org/wiki/HSV-Farbraum) für
        ///  .hue der Hue-Wert
        /// .saturation der Sättingungswert
        /// für die komplette Beschreibung des Farbraums gehört dann auch die Brightness
        ///     dazu. Also 3 Werte.
        /// Mehr in :http://www.developers.meethue.com/documentation/color-conversions-rgb-xy
        /// Bsp.: ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.brightness = brightness / 255.0);
        ///     um die Helligkeit zu setzten
        /// </summary>
        /// <param name="lampNumber">HUE-Bridge lampId</param>
        /// <param name="stateChange">mittels Lamda was geändert werden soll</param>
        public static void ChangeLampState(int lampNumber, Delegate stateChange)
        {
            HueLamp lamp;
            
            lamps.TryGetValue(lampNumber, out lamp);
            
            if (lamp == null)
            {
                Debug.WriteLine("Didn't find lamp for number " + lampNumber);
                return;
            }

            stateChange.DynamicInvoke(lamp);

            messaging.SendMessage(lamp);
        }

        /// <summary>
        /// Parameter and das Obj. HueLamp delegieren
        /// </summary>
        /// <param name="lamp">state,hue,brightness,....</param>
        public delegate void LampStateChange(HueLamp lamp);

        /// <summary>
        /// Parameter auf alle Lampen der HUE-Bridge setzen
        /// </summary>
        /// <param name="stateChange"></param>
        public static void ChangeAllLampState(Delegate stateChange)
        {
            foreach (HueLamp lamp in lamps.Values)
            {
                stateChange.DynamicInvoke(lamp);
            }
            messaging.SendMessage(lamps.Values.ToList<HueLamp>());
        }

        /// <summary>
        /// Die IP-Adr und den User/Appnamen aus der XML laden
        /// und den Var bridge und user zuzuweisen
        /// </summary>
        /// <returns>true für korrekte Werte</returns>
        public static bool LoadConfig()
        {
            // Momentan ist die Try/catch nur eine Notfalllösung
            try
            {
                XDocument doc = XDocument.Load(@"Settings.xml");

                var data = from item in doc.Descendants("settings")
                           select new
                           {
                               bridge = item.Element("bridgeip").Value,
                               user = item.Element("username").Value
                           };

                foreach (var val in data)
                {
                    bridgeIP = val.bridge;
                    username = val.user;
                    break;
                }

                Regex ipRegex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");

                bool success = (bridgeIP != null && ipRegex.IsMatch(bridgeIP) && !String.IsNullOrWhiteSpace(username));
                return success;
            }
            catch 
            {
                bridgeIP = "192.168.118.240";
                username = "26a36d65807946339133551842be44";
                return true;
            }
            //string tmp = Path.GetDirectoryName(System.Environment.CurrentDirectory);



            //Console.WriteLine("Load config returned bridge ip [" + bridgeIP + "] and username [" + username + "] and return code [" + success + "]");
            
        } 

        #endregion

        #region PSP 2.3 editLampName(int lampId, string newName)
        /// <summary>
        /// PSP 2.3
        /// Editieren einer Lampe mittels id und neuer Name
        /// </summary>
        /// <param name="lampId"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        static bool editLampName(int lampId, string newName)
        {
            bool success = false;

            return success;
        }
        #endregion

        #region PSP 3.3 deleteLamp(int lampId)
        /// <summary>
        /// PSP 3.3
        /// Lampe löschen mittels ID
        /// </summary>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static void deleteLamp(int lampId)
        {
            foreach (var lampkey in lamps.ToList())
            {
                if (lampkey.Key == lampId)
                {
                    lamps.Remove(lampkey.Key);
                }
            }
        }

        #endregion

        #region PSP 3.4 deleteLamp(string address)
        /// <summary>
        /// PSP 3.4
        /// Lampe löschen mittels Addresse
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        static void deleteLamp(string address)
        {
            getLampList();
        }
        #endregion

        #region PSP 15.4 dimLamp(int lampId, byte brightness)
        /// <summary>
        /// PSP 15.4
        /// Lampe dimmen
        /// </summary>
        /// <param name="lampId"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        static bool dimLamp(int lampId, byte brightness)
        {
            bool success = false;
            return success;
        }
        #endregion

        #region PSP 15.6 dimLamp(string lampName, byte brightness)
        /// <summary>
        /// PSP 15.6
        /// Lampe dimmen
        /// </summary>
        /// <param name="lampName"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        static bool dimLamp(string lampName, byte brightness)
        {
            bool success = false;
            return success;
        }
        #endregion
    }
}
