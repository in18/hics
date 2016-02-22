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

        private static void getLampList()
        {
            JsonLampList lampList = JsonConvert.DeserializeObject<JsonLampList>(messaging.DownloadState());
            lamps = lampList.ConvertToHueLamps();
        }
        private static void getWebClient()
        {
            messaging = new HueMessaging(bridgeIP, username);
        }
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
        private static void ChangeLampState(int lampNumber, Delegate stateChange)
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
        private delegate void LampStateChange(HueLamp lamp);

        private static void ChangeAllLampState(Delegate stateChange)
        {
            foreach (HueLamp lamp in lamps.Values)
            {
                stateChange.DynamicInvoke(lamp);
            }
            messaging.SendMessage(lamps.Values.ToList<HueLamp>());
        }
        private static bool LoadConfig()
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
        /// <param name="lamp_id"></param>
        /// <param name="new_name"></param>
        /// <returns></returns>
        static bool editLampName(int lamp_id, string new_name)
        {
            bool success = false;

            return success;
        }

        /// <summary>
        /// PSP 3.3
        /// Lampe löschen mittels ID
        /// </summary>
        /// <param name="lamp_id"></param>
        /// <returns></returns>
        static bool deleteLamp(int lamp_id)
        {
            bool success = false;
            foreach (var lampkey in lamps.ToList())
            {
                if (lampkey.Key==lamp_id)
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
    }
}
