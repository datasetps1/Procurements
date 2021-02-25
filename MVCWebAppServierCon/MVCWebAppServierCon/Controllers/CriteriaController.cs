using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class CriteriaController : Controller
    {
        private readonly SecondConnClass _context;

        public CriteriaController(SecondConnClass context)
        {
            _context = context;
        }

        // GET: Criteria/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria.FirstOrDefaultAsync(i => i.Id == id);

            if(criteria == null)
            {
                return NotFound();
            }

            return View(criteria);
        }

        [Route("Criteria/")]
        [Route("Criteria/Create")]
        // GET: Criteria/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.criteriaList = await _context.Criteria.ToListAsync();

            return View();
        }

        // POST: Criteria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Criteria/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Criteria criteria)
        {
            ViewBag.criteriaList = await _context.Criteria.ToListAsync();

            if (ModelState.IsValid)
            {
                _context.Criteria.Add(criteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(criteria);
        }

        // GET: Criteria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria.FirstOrDefaultAsync(i => i.Id == id);
            if (criteria == null)
            {
                return NotFound();
            }
            return View(criteria);
        }

        // POST: Criteria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Criteria criteria)
        {
            if (id != criteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriteriaExists(criteria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
            return View(criteria);
        }

        // GET: Criteria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criteria == null)
            {
                return NotFound();
            }

            return View(criteria);
        }

        // POST: Criteria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criteria = await _context.Criteria.FindAsync(id);
            _context.Criteria.Remove(criteria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }

        private bool CriteriaExists(int id)
        {
            return _context.Criteria.Any(e => e.Id == id);
        }
    }
}
