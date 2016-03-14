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

        ////Methoden   
        //public void AdminResetPasswordData(string newpassword, string retypepassword)
        //{
        //    this.NewPassword = newpassword;
        //    this.RetypePassword = retypepassword; 
        //}
    }
}