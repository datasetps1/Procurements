using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin, EnterPrice")]
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


        // GET: SalesQouteHeader/Create
        public async Task<ActionResult> Create(int? id)
        {
            // if the action is display (get the order with the passed id) 
            ViewBag.Criteria_List = _context.Criteria.ToList();

            if(id != null)
            {
                ViewBag.Status = "display";
                //get the order and its all realated data
                var Order_to_display = await _context.SalesQouteHeader
                    .Include(o => o.salesSuppliers)
                    .Include(o => o.salesCriterias)
                    .FirstOrDefaultAsync(o => o.Id == id);

                return View(Order_to_display);
            }

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

            //iterate files from the request => save them
            var file_list = request.Files;
            foreach (var file in file_list)
            {
                var file_key = file.Name;
                var index_to_add_on = Int16.Parse(file_key.Substring(file_key.Length-1));

                //save file
                var uniqueFileName = await save_file(file);
               
                //save path of the file
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
                else if (key.Contains("Description"))
                {
                    salesQouteHeader.Description = request[key];
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

            string uploads_folder = Path.Combine(hostingEnviroment.WebRootPath, "SalesSupplier");

            var coming_file_name = "";
            // to prevent files with very long name to throw error 
            if(file.FileName.Length > 15)
            {
                coming_file_name = file.FileName.Substring(file.FileName.Length-13);
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

        // GET: SalesQouteHeader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesQouteHeader/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {

            return Json(new { redirectToUrl = Url.Action("Search") });
        }

        // GET: SalesQouteHeader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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

            List <SupplierViewModel> s_names = new List<SupplierViewModel>();
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

            return Json(new { urlToDirect = Url.Action("Search")});
        }

    }
}