using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class AdminChangePasswordModel
    {

        //Felder & Eigenschaften

        [Required(ErrorMessage = "This field is required.", AllowEmptyStrings = false)]
        public string RecentPassword { get; set; }

        [Required(ErrorMessage = "This field is required.", AllowEmptyStrings = false)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required.", AllowEmptyStrings = false)]
        [CompareAttribute("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string RetypeNewPassword { get; set; }

        public int id { get; set; }
    }
}