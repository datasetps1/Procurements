using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class GeneralPreferenceClass
    { 
        [Key]
        public int code { get; set; }
        [Required]
        [Display(Name= "Qoute Amount")]
        public float QouteAmount { get; set; }
        [Display(Name = "Deduction Amount")]
        public float DeductionAmount { get; set; }
        [Display(Name = "Connect With")]
        public string ConnecWith { get; set; } // finpack or audit
        [Display(Name = "Project Table")]
        public string ProjectTable { get; set; }
        [Display(Name = "Activitiy Table")]
        public string ActivitiyTable { get; set; }

        [Display(Name = "link view name")]
        public string link_view_name { get; set; }


        [Display(Name = "Company Name")]
        public string Company_Name { get; set; }


        [Display(Name = "Company Logo")]
        public string Company_Logo { get; set; }

        [NotMapped]
        public List<IFormFile> Logo_Files { get; set; }
    }
}
