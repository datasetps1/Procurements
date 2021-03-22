using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class CompleteActivityFiles
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string File_path { get; set; }

        [ForeignKey("CompleteActivity")]
        public int CompleteActivityId { get; set; }
        //public CompleteActivity CompleteActivity { get; set; }
    }
}
