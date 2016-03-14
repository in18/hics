using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class AdminResetPasswordModel
    {
        //Felder & Eigenschaften
        public string NewPassword { get; set; }
        public string RetypePassword { get; set; }
        
    }
}