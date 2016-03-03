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

        public static void SetLampState(int lampId, bool onOff)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.state = onOff));
        }

        public static void SetLampBrightness(int lampId, byte brightness)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.brightness = brightness / 255.0));
        }

        public static void SetLampName(int lampId, string lampName)
        {
            HueAccess.ChangeLampState(lampId, new HueAccess.LampStateChange((HueLamp l) => l.name = lampName));
        }
        
    }
}
