using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HicsBL;
using System.ComponentModel.DataAnnotations;


namespace HicsMVC.Models
{
    public class UserAddModel
    {
        [Required(ErrorMessage = "The Username is required.")]
        //[StringLength(10, ErrorMessage = "UserName between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserName { get; set; }

        [Required(ErrorMessage = "The Password is required.")]
        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserPassword { get; set; }

        [Required(ErrorMessage = "The Password-Confirmation required.")]
        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        [CompareAttribute("NewUserPassword", ErrorMessage = "Passwords don't match.")]
        public string RetypeNewUserPassword { get; set; }

        public List<fn_show_users_Result> Userlist { get; set; }
    }
}