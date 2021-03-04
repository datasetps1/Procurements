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

        // GET: CompleteActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompleteActivity.ToListAsync());
        }

        // GET: CompleteActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completeActivity = await _context.CompleteActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (completeActivity == null)
            {
                return NotFound();
            }

            return View(completeActivity);
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

        // GET: CompleteActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completeActivity = await _context.CompleteActivity.FindAsync(id);
            if (completeActivity == null)
            {
                return NotFound();
            }
            return View(completeActivity);
        }

        // POST: CompleteActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierId,Description,OrderCode,BookName,ActivityVenue,ActivityDate,DoneTasks,CoordinatorName,ProjectName,Date")] CompleteActivity completeActivity)
        {
            if (id != completeActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(completeActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompleteActivityExists(completeActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(completeActivity);
        }

        // GET: CompleteActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completeActivity = await _context.CompleteActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (completeActivity == null)
            {
                return NotFound();
            }

            return View(completeActivity);
        }

        // POST: CompleteActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var completeActivity = await _context.CompleteActivity.FindAsync(id);
            _context.CompleteActivity.Remove(completeActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompleteActivityExists(int id)
        {
            return _context.CompleteActivity.Any(e => e.Id == id);
        }
    }
}
