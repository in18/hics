using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class UserChangePasswordModel
    {
        //Felder & Eigenschaften

        // [] = Attribute - stehen direkt darüber
        [Required(ErrorMessage = "required field", AllowEmptyStrings = false)]
        public string RecentPassword { get; set; }

        [Required(ErrorMessage = "required field", AllowEmptyStrings = false)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "required field", AllowEmptyStrings = false)]
        [CompareAttribute("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string RetypeNewPassword { get; set; }               
    }
}