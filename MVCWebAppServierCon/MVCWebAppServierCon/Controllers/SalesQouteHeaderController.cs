using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Migrations;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;
using Nancy.Json;
using Newtonsoft.Json;

namespace MVCWebAppServierCon.Controllers
{
    
    public class SalesQouteHeaderController : Controller
    {

        private IHostingEnvironment hostingEnviroment { get; }
        private readonly SecondConnClass _context;

        private readonly UserManager<IdentityUser> userManager;

        public SalesQouteHeaderController(UserManager<IdentityUser> userManager, SecondConnClass context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            this.hostingEnviroment = hostingEnviroment;

            this.userManager = userManager;
        }

        // GET: SalesQouteHeader
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalesQouteHeader/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesQouteHeader/Create
        public ActionResult Create()
        {
            ViewBag.Criteria_List = _context.Criteria.ToList();
            return View();
        }

        // POST: SalesQouteHeader/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSupliers(IFormCollection request)
        {
            string []Suppliers_list_keys = request.Keys.Where(key =>key.Contains("SupplierName")).ToArray();
            //to get number of critetias added
            string[] Criterias_list_keys = request.Keys.Where(key => key.Contains("citeria_CriteriaId")).ToArray();

            var salesQouteHeader = new SalesQouteHeader();
            var suppliers_array = new SalesSuppliers[Suppliers_list_keys.Length];
            var criterias_array = new SalesCriterias[Criterias_list_keys.Length];

            //initialize array of suppliers
            for (int i = 0; i < suppliers_array.Length; i++)
            {
                suppliers_array[i] = new SalesSuppliers();
            }

            //initialize array of criterias
            for (int i = 0; i < criterias_array.Length; i++)
            {
                criterias_array[i] = new SalesCriterias();
            }

            var file_list = request.Files;
            foreach (var file in file_list)
            {
                var file_key = file.Name;
                var index_to_add_on = Int16.Parse(file_key.Substring(file_key.Length-1));

                //save file
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "SalesSupplier"); 

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Close();
                }

                suppliers_array[index_to_add_on].AttachmentPath = uniqueFileName;
            }

            //get the suppliers and criterias values.
            foreach(string key in request.Keys)
            {
                if (key.Contains("SupplierName"))
                {
                    string name = request[key];

                    var index_to_add_on = Int16.Parse(key.Substring(key.Length - 1));

                    suppliers_array[index_to_add_on].SupplierName = name;

                    var x = name;
                }
                else if (key.Contains("citeria_CriteriaId"))
                {
                    string Id = request[key];

                    var index_to_add_on = Int16.Parse(key.Substring(key.Length - 1));

                    criterias_array[index_to_add_on].CriteriaId = Int16.Parse(Id);

                }
                else if (key.Contains("citeria_Percentage"))
                {
                    string Percentage = request[key];

                    var index_to_add_on = Int16.Parse(key.Substring(key.Length - 1));

                    criterias_array[index_to_add_on].Percentage = Int16.Parse(Percentage);
                }
                else if (key.Contains("OfferName"))
                {
                    string OfferName = request[key];

                    salesQouteHeader.OfferName = OfferName;
                }
                else if (key.Contains("OfferDate"))
                {
                    DateTime OfferDate = DateTime.Parse(request[key]);

                    salesQouteHeader.OfferDate = OfferDate;
                }
                else if (key.Contains("ExpierDate"))
                {
                    DateTime ExpierDate = DateTime.Parse(request[key]);

                    salesQouteHeader.ExpierDate = ExpierDate;
                }
            }

            //complete other salesQouteHeader info
            salesQouteHeader.CreationDate = DateTime.Now;
            //get the user id
            var user = await userManager.GetUserAsync(HttpContext.User);
            var user_id = user.Id;
            salesQouteHeader.userCode = user_id;

            //add suppliers and criterias to header object
            salesQouteHeader.salesSuppliers = suppliers_array;
            salesQouteHeader.salesCriterias = criterias_array;

            
            //add header to database
            _context.SalesQouteHeader.Add(salesQouteHeader);
            await _context.SaveChangesAsync();

            return Json(new { redirectToUrl = Url.Action("Search", "SalesQouteHeader") }); ;
        }

        private async Task<string> save_file(IFormFile file)
        {

            string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "SalesSupplier");

            var generate_file_Name = Guid.NewGuid().ToString() + "/" + file.FileName;
            var file_Path = Path.Combine(uploadsFolder, generate_file_Name);

            using (var stream = new FileStream(file_Path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }

            return generate_file_Name;
        }

        // GET: SalesQouteHeader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesQouteHeader/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesQouteHeader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesQouteHeader/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //GET : /SalesQouteHeader/search
        public ActionResult Search()
        {
            ViewBag.header_list = _context.SalesQouteHeader.ToList();
            return View();
        }

        public async Task<ActionResult> Analize(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //get header with all its suppliers and criteerias
            var header = await _context.SalesQouteHeader.FirstOrDefaultAsync(h => h.Id == id);

            if(header == null)
            {
                return NotFound();
            }

            var suppliers = await _context.SalesSuppliers.Where(s => s.salesQouteHeaderId == header.Id).ToListAsync();
            var criterias = await _context.SalesCriterias.Where(c => c.salesQouteHeaderId == header.Id).ToListAsync();

            header.salesSuppliers = suppliers;
            header.salesCriterias = criterias;

            var original_criterias = await _context.Criteria.ToListAsync();
            ViewBag.original_criterias = original_criterias;

            List < SupplierViewModel > s_names = new List<SupplierViewModel>();
            foreach(var s in suppliers)
            {
                s_names.Add(new SupplierViewModel() { Id = s.Id, SupplierName = s.SupplierName });
            }
            ViewBag.salesSuppliers = s_names;

            //pass header id to view 
            ViewBag.HeaderId = id;

            //get analysis data
            List<OrderAnalysis> OrderAnalysis_data = _context.OrderAnalysis.Where(o => o.SalesQouteHeaderId == id).ToList();
            ViewBag.OrderAnalysis_data = OrderAnalysis_data;

            return View(header);
        }

        [HttpPost]
        public ActionResult Analize(List<OrderAnalysis> list , Boolean there_was_existing_analysis , int order_id)
        {
            //if there was existing analysis for this order :
            //remove them and create new ones from the comming list :
            if (there_was_existing_analysis)
            {
                IList<OrderAnalysis> list_to_remove = _context.OrderAnalysis.Where(o => o.SalesQouteHeaderId == order_id).ToList();
                _context.OrderAnalysis.RemoveRange(list_to_remove);
            }
            _context.OrderAnalysis.AddRange(list);
            _context.SaveChanges();
            return null;
        }

    }
}