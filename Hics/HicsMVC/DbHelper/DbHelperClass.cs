using HicsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.DbHelper
{
    public class DbHelperClass
    {

        public static List<LampControl> getLamps() {
            List<LampControl> lamps = new List<LampControl>();
            LampControl lc = null;

            lc = new LampControl { id = 1, groupname = "Wohnzimmer", lampname = "WZ1", OnOff = true, dimmer = 200 };
            lamps.Add(lc);

            lc = new LampControl { id = 2, groupname = "Wohnzimmer", lampname = "WZ2", OnOff = false, dimmer = 100 };
            lamps.Add(lc);

            lc = new LampControl { id = 4, groupname = "Wohnzimmer", lampname = "WZ3", OnOff = true, dimmer = 240 };
            lamps.Add(lc);

            lc = new LampControl { id = 11, groupname = "WC", lampname = "WC1", OnOff = false, dimmer = 255 };
            lamps.Add(lc);

            lc = new LampControl { id = 15, groupname = "WC", lampname = "WC2", OnOff = true, dimmer = 230 };
            lamps.Add(lc);

            lc = new LampControl { id = 14, groupname = "Bad", lampname = "Bad1", OnOff = true, dimmer = 210 };
            lamps.Add(lc);

            lc = new LampControl { id = 41, groupname = "Bad", lampname = "Bad2", OnOff = false, dimmer = 126 };
            lamps.Add(lc);

            lc = new LampControl { id = 58, groupname = "Vorzimmer", lampname = "VZ1", OnOff = true, dimmer = 245 };
            lamps.Add(lc);

            lc = new LampControl { id = 98, groupname = "Vorzimmer", lampname = "VZ2", OnOff = true, dimmer = 255 };
            lamps.Add(lc);

            lc = new LampControl { id = 100, groupname = "Kinderzimmer", lampname = "KZi1", OnOff = false, dimmer = 214 };
            lamps.Add(lc);

            lc = new LampControl { id = 3, groupname = "Kinderzimmer", lampname = "KZi2", OnOff = true, dimmer = 236 };
            lamps.Add(lc);

            return lamps;
        

        }
    }
}