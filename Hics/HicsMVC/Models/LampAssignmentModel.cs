using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HicsBL;


namespace HicsMVC.Models
{
    public class LampAssignmentModel
    {
        #region Fields & Properties

        [Required(ErrorMessage = "Group is required.")]
        public string groupname { get; set; }

        [Required(ErrorMessage = "Lamp is required.")]
        public int lamp_id { get; set; }

        public List<fn_show_lampgroups_Result> grouplist { get; set; }

        public List<fn_show_lamps_Result> lamplist { get; set; }

        public List<fn_show_lampgroup_allocate_Result> lampAssignmentList { get; set; }

        #endregion
    }
}