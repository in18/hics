using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HicsMVC.Models
{
    public class SuperAdminModel
    {
        public string Adminname { get; set; }

        [StringLength(10,MinimumLength = 3)]
        public string Password { get; set; }

        [StringLength(10, MinimumLength = 3)]
        public string RetypePassword { get; set; }
    }
}