using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HicsMVC.Models
{
    public class LampSetupModel
    {
        //Felder & Eigenschaften
        [Required(ErrorMessage = "ID is required.")]
        public string id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Range(1, 50, ErrorMessage = "50 letters maximum.")]
        public string description { get; set; }

        //Methoden
        //public void LampData (string id, string description)
        //{
        //    this.id = id;
        //    this.description = description;
        //}
    }
}