using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Helpers
{
    public class HelperFunctions
    {
        public static async Task<string> save_file(IHostingEnvironment hostingEnviroment ,IFormFile file, string floder_name)
        {

            string uploads_folder = Path.Combine(hostingEnviroment.WebRootPath, floder_name);

            var coming_file_name = "";
            // to prevent files with very long name to throw error 
            if (file.FileName.Length > 15)
            {
                coming_file_name = file.FileName.Substring(file.FileName.Length - 13);
            }
            else
            {
                coming_file_name = file.FileName;
            }

            var file_name = Guid.NewGuid().ToString() + "_" + coming_file_name;

            string full_path = Path.Combine(uploads_folder, file_name);

            using (var stream = new FileStream(full_path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }

            return file_name;
        }
    }
}
