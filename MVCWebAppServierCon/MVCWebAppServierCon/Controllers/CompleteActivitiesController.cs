using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class CompleteActivitiesController : Controller
    {


        private IHostingEnvironment hostingEnviroment { get; }
        private readonly SecondConnClass _context;

        public CompleteActivitiesController(SecondConnClass context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            this.hostingEnviroment = hostingEnviroment;
        }

        // GET: CompleteActivities/Create
        public async Task<IActionResult> Create(int? code)
        {

            ViewBag.OrderHeader = await _context.TblOrderHeader.FirstOrDefaultAsync(o => o.OrderHeaderCode == code);

            return View();
        }

        // POST: CompleteActivities/Create
        [HttpPost]
        public ActionResult Create(CompleteActivity completeActivity, List<CompleteActivityOffered> offered_list , List<string> paths_array)
        {
            //add data to database
            completeActivity.activityOffereds = offered_list;

            //prepare files
            var activityFiles = new List<CompleteActivityFiles>();
            foreach(var current in paths_array)
            {
                activityFiles.Add(new CompleteActivityFiles()
                {
                    File_path = current
                });
            }

            completeActivity.activityFiles = activityFiles;

            _context.CompleteActivity.Add(completeActivity);
            _context.SaveChanges();

            return Json(new { direct_urk = Url.Action("StuckOrders" , "home") });
        }

        // POST: CompleteActivities/CreateFiles
        [HttpPost]
        public async Task<IActionResult> CreateFiles(IFormCollection request)
        {
            string[] paths_array = new string[request.Files.Count];

            //Helpers.HelperFunctions.save_file();
            int x = 0;
            
            for(int i=0; i< request.Files.Count; i++)
            {
                var file_path = await Helpers.HelperFunctions.save_file(hostingEnviroment, request.Files[i], "images/complete_activity");
                paths_array[i] = file_path;
            }


            return Json(new  { paths_array= paths_array });

        } 

        private bool CompleteActivityExists(int id)
        {
            return _context.CompleteActivity.Any(e => e.Id == id);
        }
    }
}
