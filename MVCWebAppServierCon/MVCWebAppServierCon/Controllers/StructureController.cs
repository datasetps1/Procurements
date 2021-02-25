using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCWebAppServierCon.Controllers
{
    public class StructureController : Controller
    {
        private readonly SecondConnClass _sc;
        //private readonly ConnnectionStringClass _cc;

        public StructureController(SecondConnClass sc)
        {
            _sc = sc;
        }
        /* public StructureController(ConnnectionStringClass cc)
         {
             _cc = cc;
         }*/
        // GET: Structure
        public ActionResult Index()
        {
            var list = _sc.TblStructure.OrderBy(s => s.structureRank).ToList();
            return View(list);
        }

        // GET: Structure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Structure/Create
        public ActionResult Create()
        {
            ViewBag.structure = _sc.TblStructure.OrderBy(s => s.structureRank).ToList();
            var mess = TempData["ErrorMessage"] as String;
            if (mess != null && mess.Equals("error"))
            {
                ModelState.AddModelError("", "You Need To define The Structures First");
            }
            return View();
        }

        // POST: Structure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StructureClass sc)
        {
            try
            {
                sc.structureUserId = 1;
                sc.structureCreationDate = DateTime.Now;
                _sc.Add(sc);
                _sc.SaveChanges();
                ViewBag.structure = _sc.TblStructure.OrderBy(s => s.structureRank).ToList();
                ViewBag.message = "The Record " + sc.structureName + " Is Saved Successfully... !";
                return View(sc);

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Structure/Edit/5
        public ActionResult Edit(int id)
        {
            var res = _sc.TblStructure.Where(s => s.structureCode == id).FirstOrDefault();
            return View(res);
        }

        // POST: Structure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StructureClass sc)
        {
            try
            {
                var res = _sc.TblStructure.Where(s => s.structureCode == id).FirstOrDefault();
                res.structureName = sc.structureName;
                res.structureName2 = sc.structureName2;
                res.structureRank = sc.structureRank;
                res.structureNote = sc.structureNote;
                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: Structure/Delete/5
        public ActionResult Delete(int id)
        {
            var del = _sc.TblStructure.Where(i => i.structureRank == id).FirstOrDefault();
            //_sc.TblItem.Remove(del);
            //_sc.SaveChanges();
            return View(del);

        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StructureClass collection)
        {
            try
            {

                var CheckAmtSett = _sc.TblAmountSitting.Where(m => m.amountStructure == id).FirstOrDefault();
                var checkUser = _sc.TblUser.Where(m => m.userTypeCode == id).FirstOrDefault();
                if (CheckAmtSett != null || checkUser != null)
                {
                    ViewBag.message = "The Structure is used";
                    return RedirectToAction(nameof(Create));
                }
                var del = _sc.TblStructure.Where(i => i.structureCode == id).FirstOrDefault();
                _sc.TblStructure.Remove(del);
                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

    }
}