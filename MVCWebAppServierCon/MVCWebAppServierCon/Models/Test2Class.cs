using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVCWebAppServierCon.Models
{
    public class Test2Class
    {
        [Key]
        public int testCode { get; set; }
        [Required]
        public String testName { get; set; }
        [Required]
        public String name2 { get; set; }
        [Required]
        public String name3 { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    }
}
