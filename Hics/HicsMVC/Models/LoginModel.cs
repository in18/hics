using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class LoginModel
    {
        //Felder & Eigenschaften

        // [] = Attribute - stehen direkt darüber
        [Required(ErrorMessage = "required field", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "required field", AllowEmptyStrings = false)]
        public string Password { get; set; }        
    }    
}