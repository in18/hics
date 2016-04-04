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
        [StringLength(50, ErrorMessage = "50 letters maximum.", MinimumLength = 1)]
        public string Groupname { get; set; }

        public List<fn_show_lampgroups_Result> GroupSetupList { get; set; }

        #endregion


    }
}