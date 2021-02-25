using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class Forms
    {
        [Key]
        public int Code { get; set; }
       
        [Required]
        public String FilePath { get; set; }
        [Required]
        public String Name { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
