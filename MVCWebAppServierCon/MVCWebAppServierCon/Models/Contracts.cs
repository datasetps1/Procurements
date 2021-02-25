using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class Contracts
    {
       
            [Key]
            public int Code { get; set; }

            [Required]
            public String FilePath { get; set; }
            [Required]
            public DateTime FromDate { get; set; }
            [Required]
            public DateTime ToDate { get; set; }
            [Required]
            public String Name { get; set; }
            [Required]
            [NotMapped]
            public IFormFile File { get; set; }

            [NotMapped]
            public int DayesNumber { get; set; }

            public string SupplierCode { get; set; }
    }
}
