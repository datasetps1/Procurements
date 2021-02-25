﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MVCWebAppServierCon.Controllers
{

    public class UserController : Controller
    {
        private readonly SecondConnClass _sc;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public UserController(SecondConnClass sc, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _sc = sc;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: User
        public ActionResult Index()
        {
            var result = _sc.TblUser.ToList();
            return View(result);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var mess = TempData["ErrorMessage"] as String;
            if (mess != null && mess.Equals("error"))
            {
                ModelState.AddModelError("", "You Need To define The Users First");
            }
            var stru = _sc.TblStructure.ToList();
            if(stru.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Structure");
            }
            ViewBag.Users = _sc.TblUser.ToList();


            ViewBag.uType = stru;
            ViewBag.DepName = _sc.TblDepartment.ToList();
            //ViewBag.uType = stru.Where(s => s.structureRank.Equals(1)).Select(us => us.structureName).ToList();
            //ViewBag.uDepMan = stru.Where(s => s.structureRank.Equals(2)).ToList(); // remove select name from these rank for Dep. Man. = 2
            //ViewBag.uMngAss = stru.Where(s => s.structureRank.Equals(3)).ToList();
            //ViewBag.uSecMan = stru.Where(s => s.structureRank.Equals(4)).ToList();
            //ViewBag.uFinDep = stru.Where(s => s.structureRank.Equals(5)).ToList();
            
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel uvc)// iusert to the user table and aspDotNetUsers table
        {
            string errormsg = "";
            if (uvc.userType.Equals(1))
            {
               // uvc.userDepartment= 0;
            }

          //  var userWithSameEmail = _sc.Users.Where(m => m.Email == uvc.userEmail).SingleOrDefault(); //checking if the emailid already exits for any user
            var userWithSameUser = _sc.Users.Where(m => m.UserName == uvc.userName).SingleOrDefault(); //checking if the emailid already exits for any user

            if (ModelState.IsValid)
            {
                //if (userWithSameEmail == null || userWithSameUser == null)
                if (userWithSameUser == null)
                {
                    var user = new IdentityUser { UserName = uvc.userName, Email = uvc.userEmail, PasswordHash = uvc.userPassword };

                    var res = await userManager.CreateAsync(user, uvc.userPassword);
                    if (res.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                    }
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                        errormsg = error.Description;
                    }
                   if (errormsg != "")
                    {

                        var stru1 = _sc.TblStructure.ToList();

                        ViewBag.uType = stru1;
                        ViewBag.DepName = _sc.TblDepartment.ToList();

                        ViewBag.Users = _sc.TblUser.ToList();
                        return View(uvc);

                    }

                    String uname = uvc.userName;
                    // int uId = _sc.TblUser.Where(u => u.userName.Equals(uname)).Select(u => u.userCode).FirstOrDefault();
                    string uId = user.Id;
                    UserClass uc = new UserClass();
                    uc.userCode = uId;
                    uc.userUserId = uId;
                    uc.userCreationDate = DateTime.Now;
                    uc.userDepartmentCode = uvc.userDepartment;
                    uc.userName = uvc.userName;

                    uc.userActive = uvc.userActive;
                    uc.userEmail = uvc.userEmail;
                    uc.userTypeCode = uvc.userType;
                    uc.userNote = uvc.userNote;
                    _sc.Add(uc);
                    _sc.SaveChanges();



                    ViewBag.message = "The Record " + uc.userName + " Is Saved Successfully... !";
                }
                else
                {
                    ViewBag.Message = "User or Email Already Exist";
                  //  return View("New");
                }
            }

            /* try
             {*/
            /*UserClass uc = new UserClass();
            uc.userUserId = 1;
            uc.userCreationDate = DateTime.Now;*/
            /* if (ModelState.IsValid)
             {
                 _sc.Add(uc);
                 _sc.SaveChanges();
                 ViewBag.Users = _sc.TblUser.ToList();
                 ViewBag.Users = _sc.TblUser.ToList();

                 var stru = _sc.TblStructure.ToList();

                 ViewBag.uType = stru.Where(s => s.structureRank.Equals(1)).Select(us => us.structureName).ToList();
                 ViewBag.uDepMan = stru.Where(s => s.structureRank.Equals(2)).Select(us => us.structureName).ToList();
                 ViewBag.uMngAss = stru.Where(s => s.structureRank.Equals(3)).Select(us => us.structureName).ToList();
                 ViewBag.uSecMan = stru.Where(s => s.structureRank.Equals(4)).Select(us => us.structureName).ToList();
                 ViewBag.uFinDep = stru.Where(s => s.structureRank.Equals(5)).Select(us => us.structureName).ToList();
                 //ViewBag.uType = new List<String> { "ahmad" };
                 //    ViewBag.uDepMan = new List<String> { "ahmad" };
                 //    ViewBag.uMngAss = new List<String> { "ahmad" };
                 //    ViewBag.uSecMan = new List<String> { "ahmad" };
                 //    ViewBag.uFinDep = new List<String> { "ahmad" };

                 ViewBag.message = "The Record " + uc.userName + " Is Saved Successfully... !";
             }*/
            var stru = _sc.TblStructure.ToList();

            ViewBag.uType = stru;
            ViewBag.DepName = _sc.TblDepartment.ToList();

            //ViewBag.uType = stru.Where(s => s.structureRank.Equals(1)).Select(us => us.structureName).ToList();
            //ViewBag.uDepMan = stru.Where(s => s.structureRank.Equals(2)).Select(us => us.structureName).ToList();
            //ViewBag.uMngAss = stru.Where(s => s.structureRank.Equals(3)).Select(us => us.structureName).ToList();
            //ViewBag.uSecMan = stru.Where(s => s.structureRank.Equals(4)).Select(us => us.structureName).ToList();
            //ViewBag.uFinDep = stru.Where(s => s.structureRank.Equals(5)).Select(us => us.structureName).ToList();

            ViewBag.Users = _sc.TblUser.ToList();
            return View(uvc);

                //return RedirectToAction(nameof(Index));
            /*}
            catch
            {
                return View();
            }*/
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            var mess = TempData["ErrorMessage"] as String;
            if (mess != null && mess.Equals("error"))
            {
                ModelState.AddModelError("", "You Need To define The Users First");
            }
            var stru = _sc.TblStructure.ToList();
            if (stru.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Structure");
            }
            //ViewBag.Users = _sc.TblUser.ToList();


            ViewBag.uType = stru;
            ViewBag.DepName = _sc.TblDepartment.ToList();

            UserViewModel uvc = new UserViewModel();

            var res = _sc.TblUser.Where(u => u.userCode == id).FirstOrDefault();
            ViewBag.Users = res;
            uvc.userActive = res.userActive;
            uvc.userDepartment = res.userDepartmentCode;
            uvc.userNote = res.userNote;
            uvc.userType = res.userTypeCode;
            uvc.userName = res.userName;
            uvc.userCode = res.userCode;
            uvc.userEmail = res.userEmail;
            return View(uvc);

        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(string id, UserViewModel uvc)
        {
            try
            {


                var stru = _sc.TblStructure.ToList();
                if (stru.Count < 1)
                {
                    TempData["ErrorMessage"] = "error";
                    return RedirectToAction("create", "Structure");
                }
                //ViewBag.Users = _sc.TblUser.ToList();


                ViewBag.uType = stru;
                ViewBag.DepName = _sc.TblDepartment.ToList();

           

                var res = _sc.TblUser.Where(u => u.userCode == id).FirstOrDefault();

                res.userName = res.userName;
                res.userEmail = uvc.userEmail;
                //res.userPassword = uc.userPassword;
                res.userTypeCode = uvc.userType;
                res.userDepartmentCode = uvc.userDepartment;
                res.userActive = uvc.userActive;
                res.userCode = res.userCode;
                res.userCreationDate = DateTime.Now;
                res.userNote = uvc.userNote;
                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
    }
}