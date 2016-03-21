using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class AdminResetPasswordModel
    {
        //Felder & Eigenschaften

        // [] = Attribute - stehen direkt darüber
        [Required(ErrorMessage = "This field is required.", AllowEmptyStrings = false)]
        public string NewPassword { get; set; }
            
            
        [Required(ErrorMessage = "This field is required.", AllowEmptyStrings = false)]
        [CompareAttribute("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string RetypePassword { get; set; }        
    }
}