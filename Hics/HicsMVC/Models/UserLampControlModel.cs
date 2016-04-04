using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HicsBL;

namespace HicsMVC.Models
{
    public class UserLampControlModel
    {
        public Nullable<int> id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<byte> bright { get; set; }

        public List<fn_show_lamp_control_Result> lamplist { get; set; }
    }
}