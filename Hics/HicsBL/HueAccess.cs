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
        #region Nichts verändern
        private static String bridgeIP;
        private static String username;
        private static HueMessaging messaging;
        private static Dictionary<int, HueLamp> lamps;

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
        static bool deleteLamp(string address)
        {
            bool success = false;

            return success;
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
