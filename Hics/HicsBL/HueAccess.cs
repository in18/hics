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

namespace HicsBL
{
    public class HueAccess
    {
        private static String bridgeIP;
        private static String username;
        private static HueMessaging messaging;
        private static Dictionary<int, HueLamp> lamps;

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

       /// <summary>
       /// PSP 2.3
       /// Editieren einer Lampe mittels id und neuer Name
       /// </summary>
       /// <param name="lamp_id"></param>
       /// <param name="new_name"></param>
       /// <returns></returns>
        static bool EditLampName(string username, string password, int lamp_id, string new_name)
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
        static bool DeleteLamp(string username, string password, int lamp_id)
        {
            bool success = false;

            return success;
        }

        /// <summary>
        /// PSP 3.4
        /// Lampe löschen mittels Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        static bool DeleteLamp(string username, string password, string address)
        {
            bool success = false;

            return success;
        }
    }
}
