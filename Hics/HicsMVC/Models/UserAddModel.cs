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
        [StringLength(10, ErrorMessage = "UserName between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserName { get; set; }
        [StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        public string NewUserPassword { get; set; }
        [StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        public string RetypeNewUserPassword { get; set; }

        public List<fn_show_users_Result> Userlist { get; set; }
    }
}