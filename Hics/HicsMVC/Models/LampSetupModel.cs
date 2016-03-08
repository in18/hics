using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HicsBL;

namespace HicsMVC.Models
{
    public class LampSetupModel
    {

        #region Fields & Properties

        [Required(ErrorMessage = "ID is required.")]
        public string id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Range(1, 50, ErrorMessage = "50 letters maximum.")]
        public string description { get; set; }

        #endregion
    }
}
 