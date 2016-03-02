using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class LoginModel
    {
        //Felder & Eigenschaften
        public string Username { get; set; }
        public string Password { get; set; }

        //Methoden
        public void LoginData(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

    }    
}