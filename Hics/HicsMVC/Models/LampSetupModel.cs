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

        /// <summary>
        /// properties
        /// </summary>
        [StringLength(6, ErrorMessage = "Only 6 letters allowed.", MinimumLength = 6)]
        [Required(ErrorMessage = "The Lamp-Id is required.")]
        public string Id { get; set; }

        [StringLength(50, ErrorMessage = "50 letters maximum.", MinimumLength = 1)]
        [Required(ErrorMessage = "A Description is required.")]
        public string Description { get; set; }
        
        public List<fn_show_lamps_Result> Lamplist { get; set; }

        #endregion
    }
}
 