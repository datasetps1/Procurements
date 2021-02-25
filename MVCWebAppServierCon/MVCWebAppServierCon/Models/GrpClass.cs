using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MVCWebAppServierCon.Models
{
    public class GrpClass
    {
        [Key]
        public String Code { get; set; }

        public String Name { get; set; }

        public String Name2 { get; set; }
    }
}
