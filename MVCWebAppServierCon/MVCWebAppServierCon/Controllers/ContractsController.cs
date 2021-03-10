using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;

namespace MVCWebAppServierCon.Controllers
{
    [Authorize(Roles = "Admin, EnterContract")]
    public class ContractsController : Controller
    {

        SqlConnection connection;
        string conString = "";
        private readonly IConfiguration configuration;

        private readonly SecondConnClass _sc;
        private IHostingEnvironment hostingEnviroment { get; }
        public ContractsController(SecondConnClass sc, IConfiguration config, IHostingEnvironment hostingEnviroment)
        {
            _sc = sc;
            this.hostingEnviroment = hostingEnviroment;

            configuration = config;
            conString = configuration.GetConnectionString("Myconnection");
            connection = new SqlConnection(conString);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.LstFiles = _sc.TblContracts.ToList();

            var getData = new getAuditData();
            IList<CodeNameModel> SuppliersList = new List<CodeNameModel>();
            SuppliersList = getData.getTableData("VAccountSuppliers", "", connection);
            ViewBag.lstsuppliers = SuppliersList;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Contracts model)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //if (ModelState.IsValid)
            //{

            string uniqueFileName = null;

            if (model.File != null)
            {

                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Contracts");
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

                Contracts ContractTPost = new Contracts
                {
                    Name = model.Name,
                    FilePath = uniqueFileName,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    SupplierCode = model.SupplierCode,
                };

                _sc.Add(ContractTPost);
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

                var file = _sc.TblContracts.Where(m => m.Code == id).FirstOrDefault();
                if (file != null)
                {
                    var path = Path.Combine(
                              Directory.GetCurrentDirectory(),
                              "wwwroot" + "\\Contracts", file.FilePath);


                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        ViewBag.deleteSuccess = "true";
                    }
                    _sc.TblContracts.Remove(file);
                    _sc.SaveChanges();
                }
                //   return RedirectToAction(nameof(Create));
                return new JsonResult(new { success = true, data = "OK" });

            }
            catch
            {
                //  return View();
                return Json(new { message = "error " });

            }
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = _sc.TblContracts.Where(m => m.Code == id).FirstOrDefault();
            if (file != null)
            {
                var path = Path.Combine(
                          Directory.GetCurrentDirectory(),
                          "wwwroot" + "\\Contracts", file.FilePath);

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