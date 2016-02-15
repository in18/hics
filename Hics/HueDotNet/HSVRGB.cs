using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace huedotnet
{
    class HSVRGB
    {
        /// <summary>
        /// Konvertiere RGB nach HSL Farbsystem
        /// </summary>
        /// <param name="r">Rotwert 0-255</param>
        /// <param name="g">Grünwert 0-255</param>
        /// <param name="b">Blauwert 0-255</param>
        /// <param name="hue">Variable als OUT, auf welche der HUE-Wert gespeichert wird</param>
        /// <param name="saturation">Variable als OUT, auf welche der Sättigungs-Wert gespeichert wird</param>
        /// <param name="luminance">Variable als OUT, auf welche der Helligkeits-Wert gespeichert wird</param>
        public void ConvertToHSL(int r, int g, int b, out double hue, out double saturation, out double luminance)
        {
            Color newColor = Color.FromArgb(r, g, b);
            hue = newColor.GetHue();
            saturation = newColor.GetSaturation();
            luminance = newColor.GetBrightness();
        }

        /// <summary>
        /// Konvertier HSL zu RGB Farb-System
        /// Die math. Formeln sind vom Internet
        /// </summary>
        /// <param name="hue">Grad-Parameter in der HSL-Farb-Skala. 0-0.9999</param>
        /// <param name="sat">Stättigungswert 0-255</param>
        /// <param name="bri">Helligkeit 0-255</param>
        /// <param name="r">Out Variable für Rot-Wert</param>
        /// <param name="g">Out Variable für Grün-Wert</param>
        /// <param name="b">Out Variable für Blau-Wert</param>
        public void ConvertToRGB(double hue, double sat, double bri, out int r, out int g, out int b)
        {
            double rprime, gprime, bprime;

            double chroma = sat * bri;
            double hueprime = hue / 6.0;
            double x = chroma * (1 - Math.Abs((hueprime % 2) - 1));

            int hueprimeround = (int) Math.Floor(hueprime);
            switch (hueprimeround)
            {
                case 0:
                    rprime = chroma;
                    gprime = x;
                    bprime = 0;
                    break;
                case 1:
                    rprime = x;
                    gprime = chroma;
                    bprime = 0;
                    break;
                case 2:
                    rprime = 0;
                    gprime = chroma;
                    bprime = x;
                    break;
                case 3:
                    rprime = 0;
                    gprime = x;
                    bprime = chroma;
                    break;
                case 4:
                    rprime = x;
                    gprime = 0;
                    bprime = chroma;
                    break;
                case 5:
                    rprime = chroma;
                    gprime = 0;
                    bprime = x;
                    break;
                default:
                    rprime = 0;
                    gprime = 0;
                    bprime = 0;
                    break;
            }

            double m = bri - chroma;

            r = (int) Math.Round((rprime + m) * 255);
            g = (int) Math.Round((gprime + m) * 255);
            b = (int) Math.Round((bprime + m) * 255);
        }
    }
}
