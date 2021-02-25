using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class GeneralPrefController : Controller
    {

        private readonly SecondConnClass _context;
        public GeneralPrefController(SecondConnClass sc)
        {
            _context = sc;
        }

        [HttpGet("GeneralPref/")]
        [HttpGet("GeneralPref/index")]
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.TblGeneralPreference.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GeneralPreferenceClass model)
        {

             var Gpref = _context.TblGeneralPreference.FirstOrDefault();
             if (Gpref != null)
            {
                Gpref.QouteAmount = model.QouteAmount;
                Gpref.DeductionAmount = model.DeductionAmount;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(model);
                _context.SaveChanges();
            }
               
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model_to_edit = await _context.TblGeneralPreference.FirstOrDefaultAsync(g => g.code == id);

            if (model_to_edit == null)
            {
                return NotFound();
            }

            return View(model_to_edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GeneralPreferenceClass model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TblGeneralPreference.Any(g=>g.code == model.code))
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

    }
}