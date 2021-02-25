using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class GeneralPreferenceClass
    {

        [Key]
        public int code { get; set; }
        [Required]
        public float QouteAmount { get; set; }
        public float DeductionAmount { get; set; }
    }
}
