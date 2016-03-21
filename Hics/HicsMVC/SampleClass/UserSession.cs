using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.SampleClass
{
    public class UserSession
    {
        public int ID { get; set; }
        public bool admin { get; set; }
        public string name { get; set; }
        public string pw { get; set; }
    }
}