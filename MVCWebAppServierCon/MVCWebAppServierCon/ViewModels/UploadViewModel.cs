using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class UploadViewModel
    {
        
        public IFormFile uploadPath { get; set; }

        public String uploadNote { get; set; }
    }
}
