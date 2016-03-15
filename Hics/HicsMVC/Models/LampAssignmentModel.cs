﻿using HicsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.Models
{
    public class LampAssignmentModel
    {
        #region Fields & Properties

        public string groupselect { get; set; }

        public string lampselect { get; set; }

        public List<fn_show_lampgroups_Result> Grouplist { get; set; }
        public List<fn_show_lamps_Result> Lamplist { get; set; }

        #endregion
    }
}