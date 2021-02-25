using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class FormsController : Controller
    {
        private readonly SecondConnClass _sc;
        private IHostingEnvironment hostingEnviroment { get; }
        public FormsController(SecondConnClass sc, IHostingEnvironment hostingEnviroment)
        {
            _sc = sc;
            this.hostingEnviroment = hostingEnviroment;
        }
        public IActionResult Index(Forms model)
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.LstFiles = _sc.Forms.ToList();

            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult>  Create(Forms model)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //if (ModelState.IsValid)
            //{
           
                string uniqueFileName = null;

                if (model.File != null)
                {
                 
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "forms");
                //if (!Directory.Exists(uploadsFolder))
                //{
                //     Directory.CreateDirectory(uploadsFolder);
                //}
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //  model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                    stream.Close();
                }
                Forms f = new Forms
                {
                    Name = model.Name,
                    FilePath = uniqueFileName,

                };

                _sc.Add(f);
                _sc.SaveChanges();
             
            }


            return RedirectToAction(nameof(Create));

            // return RedirectToAction("details", new { id = newEmployee.Id });
            //}


            //return View();

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                var file = _sc.Forms.Where(m => m.Code == id).FirstOrDefault();
                if (file != null)
                {
                    var path = Path.Combine(
                              Directory.GetCurrentDirectory(),
                              "wwwroot" + "\\forms", file.FilePath);


                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        ViewBag.deleteSuccess = "true";
                    }
                    _sc.Forms.Remove(file);
                    _sc.SaveChanges();
                }
                //   return RedirectToAction(nameof(Create));
                return new JsonResult(new { success = true, data = "OK" });

            }
            catch
            {
                //  return View();
                return Json(new { message = "error "});

            }
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = _sc.Forms.Where(m => m.Code == id).FirstOrDefault();
            if (file != null)
            {
                var path = Path.Combine(
                          Directory.GetCurrentDirectory(),
                          "wwwroot" + "\\forms", file.FilePath);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            else
            {
                return Content("filename not present");
            }
            // return  RedirectToAction();
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}