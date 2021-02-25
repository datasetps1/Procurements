using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class SalesSuppliers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public string AttachmentPath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int salesQouteHeaderId { get; set; }
        public SalesQouteHeader salesQouteHeader { get; set; }
    }
}
