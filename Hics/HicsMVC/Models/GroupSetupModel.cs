using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HicsBL;

namespace HicsMVC.Models
{
    public class GroupSetupModel
    {

        #region Fields & Properties

        [Required(ErrorMessage = "Description is required.")]
        [Range(1, 51, ErrorMessage = "50 letters maximum.")]
        public string groupname { get; set; }

        #endregion


    }
}