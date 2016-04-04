using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class UserLampControl
    {
        public Nullable<int> id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<byte> bright { get; set; }
    }
}