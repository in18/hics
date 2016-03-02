using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class UserChangePasswordModel
    {
        //Felder & Eigenschaften
        public string RecentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RetypeNewPassword { get; set; }

        ////Methoden   
        //public void UserChangePassworData(string recentpassword, string newpassword, string retypenewpassword)
        //{
        //    this.RecentPassword = recentpassword;
        //    this.NewPassword = newpassword;
        //    this.RetypeNewPassword = RetypeNewPassword;
        //}
    }
}