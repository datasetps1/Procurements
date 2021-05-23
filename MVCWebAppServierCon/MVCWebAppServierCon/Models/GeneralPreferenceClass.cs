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

        [Display(Name = "Display Name Project")]
        public string Display_Name_Project { get; set; }

        [Display(Name = "Activitiy Table")]
        public string ActivitiyTable { get; set; }

        [Display(Name = "Display Name Activity")]
        public string Display_Name_Activityt { get; set; }

        [Display(Name = "link view name")]
        public string link_view_name { get; set; }


        [Display(Name = "Company Name")]
        public string Company_Name { get; set; }


        [Display(Name = "Company Logo")]
        public string Company_Logo { get; set; }

        [NotMapped]
        public List<IFormFile> Logo_Files { get; set; }

        [Display(Name = "Show Unit")]
        public bool Show_Unit { get; set; }

        [Display(Name = "Show Order Type")]
        public bool Show_Order_Type { get; set; }

        [Display(Name = "Show Doner2")]
        public bool Show_Doner2 { get; set; }

        [Display(Name = "Display Name Doner2")]
        public string Display_Name_Doner2 { get; set; }

        [Display(Name = "Table Name Doner2")]
        public string Table_Name_Doner2 { get; set; }

        [Display(Name = "Show cost3")]
        public bool Show_cost3 { get; set; }

        [Display(Name = "Display Name cost3")]
        public string Display_Name_cost3 { get; set; }

        [Display(Name = "Table Name cost3")]
        public string Table_Name_cost3 { get; set; }

        [Display(Name = "Show cost4")]
        public bool Show_cost4 { get; set; }

        [Display(Name = "DispTablelay Name cost4")]
        public string Display_Name_cost4 { get; set; }

        [Display(Name = "Table Name cost4")]
        public string Table_Name_cost4 { get; set; }

        [Display(Name = "Show ToDate")]
        public bool Show_ToDate { get; set; }

        [Display(Name = "Show To Employee")]
        public bool Show_ToEmployee { get; set; }
    }
}
