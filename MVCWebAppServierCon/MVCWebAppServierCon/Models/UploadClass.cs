using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVCWebAppServierCon.Models
{
    public class UploadClass
    {
        [Key]
        public int uploadCode { get; set; }
        [Required]
        public int uploadHeaderCode { get; set; }
        [Required]
        public String uploadPath { get; set; }

        public String uploadNote { get; set; }

        [Required]
        public string uploadUserId { get; set; }
        [Required]
        public DateTime uploadCreationDate { get; set; }
        [Required]
        public int uploadDeviceIp { get; set; }
    }
}
