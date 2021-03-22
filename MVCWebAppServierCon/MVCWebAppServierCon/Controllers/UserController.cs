using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace MVCWebAppServierCon.Controllers
{

    public class UserController : Controller
    {
        private readonly SecondConnClass _sc;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private IHostingEnvironment hostingEnviroment { get; }

        string conString = "";
        SqlConnection connection;

        public UserController(SecondConnClass sc, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IHostingEnvironment hostingEnviroment)
        {
            _sc = sc;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
      
            configuration = config;
            conString = configuration.GetConnectionString("ProcurementConn");
            connection = new SqlConnection(conString);
            this.hostingEnviroment = hostingEnviroment;
        }

        // GET: User
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Index()
        {
            var result = _sc.TblUser.ToList();
            return View(result);
        }

        // GET: User/Details/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [Authorize(Roles = "Admin, EnterSet")]
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
        [Authorize(Roles = "Admin, SaveSet")]
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
                    uc.Excutable = uvc.Excutable == "true" ? true : false;

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

        [HttpGet]
        //public async Task<IActionResult> EditUsersView(string id)
        //{
        //    ViewBag.roleId = id;

        //    var UserRole = await roleManager.FindByIdAsync(id);

        //    if (UserRole == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
        //        return View("NotFound");
        //    }

        //    var model = new List<VUserRoles>();

        //    foreach (var user in userManager.Users)
        //    {
        //        var VUserRoles = new VUserRoles
        //        {
        //            UserId = user.Id,
        //            UserName = user.UserName
        //        };

        //        //if (await userManager.IsInRoleAsync(user, UserRole.Name))
        //        //{
        //        //    VUserRoles.IsSelected = true;
        //        //}
        //        //else
        //        //{
        //        //    VUserRoles.IsSelected = false;
        //        //}

        //        model.Add(VUserRoles);
        //    }

        //    return View(model);
        //}

        [HttpGet]
       //  public List<CostsViewModel> projLoad(String tblName)
        public List<VUserRoles> EditUsersViewLst(String id)
        {
            //  VUserRoles vur = new VUserRoles();
            //  List<VUserRoles> UsersRolesLst = new List<VUserRoles>();
            //  var res = _sc.VUsersRole.Where(u => u.UserId == id).FirstOrDefault();
            //  ViewBag.UsersRole = res;
            //  ViewBag.userroleName = res.Name;

            ////  UsersRolesLst.Add(ViewBag.userroleName);
            //  return UsersRolesLst.to ;
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT UserId ,Name, UserName  FROM VUsersRole where UserId = '" + id + "';", connection);
            var reader = command.ExecuteReader();
            List<VUserRoles> costLst = new List<VUserRoles>();
            while (reader.Read())
            {
                VUserRoles costs = new VUserRoles();
                costs.Name = reader.GetValue(1).ToString();
                ViewBag.UserName = reader.GetValue(2).ToString();
                costLst.Add(costs);

                // do something with 'value'
            }
            connection.Close();
            return costLst;

        }

        [HttpGet]

        public ActionResult EditUsersView(String id)
        {
            //VUserRoles vur = new VUserRoles();
   
            //var res = _sc.VUsersRole.Where(u => u.UserId == id).FirstOrDefault();
            //ViewBag.UsersRole = res;
            //ViewBag.userroleName = res.Name;

            ////  UsersRolesLst.Add(ViewBag.userroleName);
            //return View(vur);

            ViewBag.userId = id;

            VUserRoles uvc = new VUserRoles();

         
            ViewBag.Name = EditUsersViewLst(id);
        

            // var res2 = AspNetRoleManager<AspNetUserManager>.Where(u => u.UserId == id).FirstOrDefault();
            // ViewBag.UserRole = res2;

            return View(uvc);

        }


        //public ActionResult getUserRoles(string UserId)
        //{
        //    var model = new VUserRoles();
        //    var res = _sc.VUsersRole.Where(o => o.UserId == UserId).FirstOrDefault();
        //    if (res.RoleId != "")
        //    {
        //      return View(model);
        //    }
        //    else
        //    {
        //        return Json(new { data = "hidden" });
        //    }

        //}

        public ActionResult EditRole11(string id)
        {
            ViewBag.userId = id;

            UserViewModel uvc = new UserViewModel();

            var res = _sc.TblUser.Where(u => u.userCode == id).FirstOrDefault();
            ViewBag.Users = res;
            uvc.userName = res.userName;
            uvc.userEmail = res.userEmail;

           // var res2 = AspNetRoleManager<AspNetUserManager>.Where(u => u.UserId == id).FirstOrDefault();
           // ViewBag.UserRole = res2;
            
            return View(uvc);
        }

        // GET: User/Edit/5
        [Authorize(Roles = "Admin, EnterSet")]
 
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
        [Authorize(Roles = "Admin, SaveSet")]

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
        [Authorize(Roles = "Admin, DeleteSet")]

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, DeleteSet")]
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