﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HicsMVC.Models
{
    public class SuperAdminModel
    {
        public string Adminname { get; set; }

        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        [Required(ErrorMessage = "The Password is required.")]
        public string Password { get; set; }

        //[StringLength(10, ErrorMessage = "Password between 3 and 10 digits.", MinimumLength = 3)]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RetypePassword { get; set; }

    }
}