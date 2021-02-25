using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCWebAppServierCon.Controllers
{
    public class GroupController : Controller
    {
        private readonly ConnnectionStringClass _cc;

        public GroupController(ConnnectionStringClass cc)
        {
            _cc = cc;
        }
        public IActionResult Index()
        {
            // var results = _cc.TBLGroups.ToList();
            //return View(results);
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GrpClass gc)
        {
            _cc.Add(gc);
            _cc.SaveChanges();
            ViewBag.message = "The Record " + gc.Name + " Is Saved Successfully... !";
            return View(gc);
        }
    }
}