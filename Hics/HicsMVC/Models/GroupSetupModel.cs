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
        public string Groupname { get; set; }

        public List<fn_show_lampgroups_Result> Grouplist { get; set; }
        #endregion


    }
}