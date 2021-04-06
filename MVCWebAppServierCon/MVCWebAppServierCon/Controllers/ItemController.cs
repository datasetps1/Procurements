using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCWebAppServierCon.Controllers
{
    public class ItemController : Controller
    {
        private readonly SecondConnClass _sc;

        public ItemController(SecondConnClass sc)
        {
            _sc = sc;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Base64String = _sc.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _sc.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }


        // GET: Item
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Index()
        {
            ViewBag.Items = _sc.TblItem.ToList();

            return View();
        }

        // GET: Item/Details/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Item/Create
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Create()
        {

            ViewBag.Units = _sc.Units.ToList();
            ViewBag.Show_Unit = _sc.TblGeneralPreference.Select(g => g.Show_Unit).FirstOrDefault();

            var mess = TempData["ErrorMessage"] as String;
            if (mess != null && mess.Equals("error"))
            {
                ModelState.AddModelError("", "You Need To define The Items First");
            }
            ViewBag.Items = _sc.TblItem.ToList();
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Create(ItemClass ic)
        {
            try
            {
                // TODO: Add insert logic here
                ic.itemUserId = 1;
                ic.itemCreationDate = DateTime.Now;
                _sc.Add(ic);
                _sc.SaveChanges();

                ViewBag.Items = _sc.TblItem.ToList();
                ViewBag.message = "The Record " + ic.itemName + " has been added";

                ViewBag.Units = _sc.Units.ToList();

                return View(ic);
            }
            catch
            {
                return View(ic);
            }
        }

        // GET: Item/Edit/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Edit(int id)
        {
            ViewBag.Show_Unit = _sc.TblGeneralPreference.Select(g => g.Show_Unit).FirstOrDefault();
            ViewBag.Units = _sc.Units.ToList();
            var res = _sc.TblItem.Where(i => i.itemCode == id).FirstOrDefault();
            return View(res);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Edit(int id, ItemClass ic)
        {
            try
            {
                var res = _sc.TblItem.Where(i => i.itemCode == id).FirstOrDefault();
                res.itemName = ic.itemName;
                res.itemName2 = ic.itemName2;
                res.itemPrice = ic.itemPrice;
                res.itemNote = ic.itemNote;
                res.Unit_Id = ic.Unit_Id;
                _sc.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int id)
        {
            ViewBag.Show_Unit = _sc.TblGeneralPreference.Select(g => g.Show_Unit).FirstOrDefault();
            ViewBag.Units = _sc.Units.ToList();
            var del = _sc.TblItem.Where(i => i.itemCode == id).FirstOrDefault();
            /*_sc.TblItem.Remove(del);
            _sc.SaveChanges();*/
            return View(del);
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, DeleteSet")]

        public ActionResult Delete(int id, ItemClass collection)
        {
            try
            {

                var CheckOrd = _sc.TblTransaction.Where(m => m.TransactionItemCode == id).FirstOrDefault();
                if (CheckOrd != null)
                {
                    ViewBag.message = "The Item is used";
                    return RedirectToAction(nameof(Create));
                }
                var del = _sc.TblItem.Where(i => i.itemCode == id).FirstOrDefault();
                _sc.TblItem.Remove(del);
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