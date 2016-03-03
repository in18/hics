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

namespace HicsBL
{
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
        internal static void getLampList()
        {
            JsonLampList lampList = JsonConvert.DeserializeObject<JsonLampList>(messaging.DownloadState());
            lamps = lampList.ConvertToHueLamps();
        }
        internal static void getWebClient()
        {
            messaging = new HueMessaging(bridgeIP, username);
        }
        internal static double GetCurrentLampBrightness(int lampNumber)
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
        /// <returns></returns>
        internal static string GetLampName(int lampNumber)
        {
            HueLamp lamp;
            lamps.TryGetValue(lampNumber, out lamp);
            return lamp.name;
        }

        /// <summary>
        /// Eine LampenId aus der aktuellen HUE-Bridge anhand des Lampennamens zu bekommen
        /// </summary>
        /// <param name="lampName">HUE-Bridge Lampenname</param>
        /// <returns></returns>
        internal static int GetLampId(string lampName)
        {
            
            int lId = 0;
            getLampList();

            for (int i = 0; i < lamps.Count; i++)
            {
                if (lamps[i].name == lampName)
                {
                    lId = lamps[i].GetLampNumber();
                }
            }
            return lId;
        }

        /// <summary>
        /// Mit dieser Methode können einzelne Werte einer HUE-Lampe gesetzt werden
        /// .brightness die Helligkeit
        /// Der HSV-Farbraum (https://de.wikipedia.org/wiki/HSV-Farbraum)
        /// .hue der Hue-Wert
        /// .saturation der Sättingungswert
        /// für die komplette Beschreibung des Farbraums gehört dann auch die Brightness
        ///     dazu. Also 3 Werte.
        /// Mehr in :http://www.developers.meethue.com/documentation/color-conversions-rgb-xy
        /// Bsp.: ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.brightness = brightness / 255.0);
        ///     um die Helligkeit zu setzten
        /// </summary>
        /// <param name="lampNumber">HUE-Bridge lampId</param>
        /// <param name="stateChange">mittels Lamda was geändert werden soll</param>
        internal static void ChangeLampState(int lampNumber, Delegate stateChange)
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
        internal delegate void LampStateChange(HueLamp lamp);

        internal static void ChangeAllLampState(Delegate stateChange)
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
        /// <returns></returns>
        internal static bool LoadConfig()
        {
            XDocument doc = XDocument.Load("Settings.xml");

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

            //Console.WriteLine("Load config returned bridge ip [" + bridgeIP + "] and username [" + username + "] and return code [" + success + "]");
            return success;
        } 

        #endregion

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

        /// <summary>
        /// PSP 3.3
        /// Lampe löschen mittels ID
        /// </summary>
        /// <param name="lampId"></param>
        /// <returns></returns>
        static bool deleteLamp(int lampId)
        {
            bool success = false;
            foreach (var lampkey in lamps.ToList())
            {
                if (lampkey.Key==lampId)
                {
                    lamps.Remove(lampkey.Key);
                }
            }
            return success;
        }

        /// <summary>
        /// PSP 3.4
        /// Lampe löschen mittels Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        static void deleteLamp(string address)
        {
            
        }

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
    }
}
