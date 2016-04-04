using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HicsBL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HicsMVC.Models
{
    public class UserAddModel
    {
        [Required(ErrorMessage = "Username required")]
        [DisplayName("UserName")]
        //[StringLength(10, ErrorMessage = "UserName between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserName { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [DisplayName("UserPassword")]
        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserPassword { get; set; }

        [Required(ErrorMessage = "New password required")]
        [DataType(DataType.Password)]
        [DisplayName("NewUserPassword")]
        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        [CompareAttribute("NewUserPassword", ErrorMessage = "Passwords don't match.")]
        public string RetypeNewUserPassword { get; set; }

        public List<fn_show_users_Result> Userlist { get; set; }
    }
}