using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace huedotnet
{
    public class HueLamp
    {
        private int lampNumber;
        public String name { get; set; }
        public bool state { get; set; }
        public double hue { get; set; }
        public double saturation { get; set; }
        public double brightness { get; set; }
        private int? transitionTime = null;

        public HueLamp(int lampNumber, String name, bool state, double hue, double sat, double bri)
        {
            this.lampNumber = lampNumber;
            this.name = name;
            this.state = state;
            this.hue = hue;
            this.saturation = sat;
            this.brightness = bri;
        }

        public int GetLampNumber()
        {
            return this.lampNumber;
        }

        /// <summary>
        /// Farbwert einer Lampe mittels RGB-System setzen
        /// </summary>
        /// <param name="r">Rot-Wert</param>
        /// <param name="g">Grün-Wert</param>
        /// <param name="b">Blau-Wert</param>
        public void SetColor(int r, int g, int b)
        {
            HSVRGB hsvrgb = new HSVRGB();
            double hue, sat, bri;

            hsvrgb.ConvertToHSL(r, g, b, out hue, out sat, out bri);

            this.hue = hue;
            this.saturation = sat;
            this.brightness = bri;
        }

        /// <summary>
        /// Den Hue-Wert setzen
        /// </summary>
        /// <param name="hue">Hue-Wert</param>
        public void SetHue(double hue)
        {
            

            this.hue = hue;
            //this.saturation = sat;
            //this.brightness = bri;
        }

        public void SetTransitionTime(int timeMilli)
        {
            this.transitionTime = (int) Math.Round(timeMilli / 100d);
        }

        public int? GetTransitionTime()
        {
            return this.transitionTime;
        }

        /// <summary>
        /// Einen String für das Json-Kommando zusammenbasteln
        /// Da steht drin on/off,brightness,hue,sättigung,....
        /// </summary>
        /// <returns></returns>
        public String GetJson()
        {
            ArrayList commands = new ArrayList();
            commands.Add("\"on\": " + (state == true ? "true" : "false"));

            commands.Add("\"hue\":" + Math.Round(hue * UInt16.MaxValue));
            commands.Add("\"sat\":" + Math.Round(saturation * 256));
            commands.Add("\"bri\": " + Math.Round(brightness * 256));
            
            if (transitionTime != null) commands.Add("\"transitiontime\": " + transitionTime);

            return String.Concat("{", String.Join(", ", commands.ToArray()), "}");
        }

    }
}
