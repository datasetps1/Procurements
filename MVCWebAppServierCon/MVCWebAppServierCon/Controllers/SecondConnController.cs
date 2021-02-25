using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;




namespace MVCWebAppServierCon.Controllers
{
    public class SecondConnController : Controller
    {
        private readonly SecondConnClass _sc;

        public SecondConnController(SecondConnClass sc)
        {
            _sc = sc;
        }
        public IActionResult Index()
        {
            var results = _sc.TblUser.ToList();
            return View(results);
        }


        //-----------------------------------------------------------------
        //public IActionResult StructureList()
        //{
        //    var results = _sc.TblStructure.ToList();
            
        //    return View(results);
        //}

        //public IActionResult CreateStructure()
        //{
        //    ViewBag.structure = _sc.TblStructure.ToList();
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateStructure(StructureClass sc)
        //{
        //    _sc.Add(sc);
        //    _sc.SaveChanges();
        //    ViewBag.structure = _sc.TblStructure.ToList();
        //    ViewBag.message = "The Record " + sc.structureName + " Is Saved Successfully... !";
        //    return View(sc);
        //}
        //--------------------------------------------------------------------------------
        public IActionResult UserList()
        {
            //var results = _sc.TblUser.ToList();
            //return View(results);
            ViewBag.users = _sc.TblUser.ToList();
            return View();
        }
        public IActionResult CreateUser()
        {
            
            ViewBag.Users = _sc.TblUser.ToList();
            
            var stru = _sc.TblStructure.ToList();
           
            ViewBag.uType = stru.Where(s => s.structureRank.Equals(1)).Select(us=> us.structureName).ToList();
            ViewBag.uDepMan = stru.Where(s => s.structureRank.Equals(2)).Select(us=> us.structureName).ToList();
            ViewBag.uMngAss = stru.Where(s => s.structureRank.Equals(3)).Select(us=> us.structureName).ToList();
            ViewBag.uSecMan = stru.Where(s => s.structureRank.Equals(4)).Select(us=> us.structureName).ToList();
            ViewBag.uFinDep = stru.Where(s => s.structureRank.Equals(5)).Select(us=> us.structureName).ToList();
            //ViewBag.uType = stru;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserClass uc)
        {
            _sc.Add(uc);
            _sc.SaveChanges();
            ViewBag.Users = _sc.TblUser.ToList();
            var stru = _sc.TblStructure.ToList();

            ViewBag.uType = stru.Where(s => s.structureRank.Equals(1)).Select(us => us.structureName).ToList();
            ViewBag.uDepMan = stru.Where(s => s.structureRank.Equals(2)).Select(us => us.structureName).ToList();
            ViewBag.uMngAss = stru.Where(s => s.structureRank.Equals(3)).Select(us => us.structureName).ToList();
            ViewBag.uSecMan = stru.Where(s => s.structureRank.Equals(4)).Select(us => us.structureName).ToList();
            ViewBag.uFinDep = stru.Where(s => s.structureRank.Equals(5)).Select(us => us.structureName).ToList();

            ViewBag.message = "The Record " + uc.userName + " Is Saved Successfully... !";
            return View(uc);
        }


    }
}