using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HicsMVC.Models
{
    public class LampControl
    {
        public int id { get; set; }

       
        public string groupname { get; set; }

        public string lampname { get; set; }

        public bool OnOff { get; set; }

        public byte dimmer { get; set; }

        

    }
}