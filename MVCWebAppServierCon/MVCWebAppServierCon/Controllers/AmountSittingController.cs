using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MVCWebAppServierCon.Controllers
{
    [Authorize]
    public class AmountSittingController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly SecondConnClass _sc;
        //private readonly ConnnectionStringClass _sc;

        public AmountSittingController(SecondConnClass sc)
        {
            _sc = sc;
        }
        /* public AmountSittingController(ConnnectionStringClass cc)
        {
            _sc = cc;
        }*/

        // GET: AmountSitting

        [AllowAnonymous]
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Index()
        {
            var result = _sc.TblAmountSitting.ToList();
            return View(result);
        }

        // GET: AmountSitting/Details/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Details(int id)
        {
            var res = _sc.TblAmountSitting.Where(r => r.amountCode == id).FirstOrDefault();
            return View(res);
        }

        // GET: AmountSitting/Create
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Create()
        {
            List<StructureClass> stru = _sc.TblStructure.OrderBy(x => x.structureRank).ToList();
            if (stru.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Structue");
            }
            ViewBag.Amount = _sc.TblAmountSitting.ToList();

            //ViewBag.Structure = _sc.TblStructure.ToList().Select(s=> s.structureName).ToList();
            ViewBag.Structure = stru;
            ViewBag.Ranks = _sc.TblAmountSitting.GroupBy(a => a.amountFrom).ToList();
            ViewBag.lop = stru.Count();

            return View();
        }

        // POST: AmountSitting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Create(AmountSittingClass asc)
        {
            try
            {
                //var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userCode).FirstOrDefault();
                //asc.amountUserId = user;
                //_sc.Add(asc);
                //_sc.SaveChanges();
                ViewBag.message = "The Record " + asc.amountStructure + " Is Saved Successfully... !";
                ViewBag.Amount = _sc.TblAmountSitting.ToList();
                ViewBag.Structure = _sc.TblStructure.OrderBy(x => x.structureRank).ToList(); ;
                ViewBag.Ranks = _sc.TblAmountSitting.GroupBy(a => a.amountFrom).ToList();
                ViewBag.lop = _sc.TblStructure.ToList().Count();

                return View(asc);

                //return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                ViewBag.message = "ERROR";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin, SaveSet")]
        public Task<IActionResult> InsertRow()
        {

            return null;
        }
        [Authorize(Roles = "Admin, SaveSet")]

        public ActionResult StruArray(int[] strArray, int fromAmount, int toAmount, string amountNote)
        {
            if (strArray != null && strArray.Length > 0)
            {
                // return Json(new { message = "ok" });

                var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userCode).FirstOrDefault();

                for (var i = 0; i < strArray.Length; i++)
                {
                    AmountSittingClass asc = new AmountSittingClass();
                    asc.amountFrom = fromAmount;
                    asc.amountTo = toAmount;
                    asc.amountCreationDate = DateTime.Now;
                    asc.amountUserId = user;
                    asc.amountStructure = strArray[i];
                    asc.amountNote = amountNote;

                    _sc.Add(asc);
                    _sc.SaveChanges();
                    //Create(asc);
                }
                //  _sc.SaveChanges();

                //some code                
                return Json(new { message = "ok" });
                //return null;
            }

            //The following code will run if it's not successful. 
            return Json(new { message = "There must be at least one Structure Name" });
        }

        // GET: AmountSitting/Edit/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Edit(int id)// this take amountFrom and delete its values then insert the new values 
        {
            var res = _sc.TblAmountSitting.Where(a => a.amountFrom == id).ToList();
            var res1 = _sc.TblAmountSitting.Where(a => a.amountFrom == id).FirstOrDefault();

            ViewBag.Structure = _sc.TblStructure.OrderBy(x => x.structureRank).ToList();
            ViewBag.amountRowLst = res;

            return View(res1);
        }

        // POST: AmountSitting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Edit(int id, AmountSittingClass asc)
        {
            try
            {

                return View();
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult EditAmSit(int[] strArray, int fromAmountPrevious, int fromAmount, int toAmount, string amountNote)
        {
            try
            {
                DeleteStrArr(fromAmountPrevious);
                StruArray(strArray, fromAmount, toAmount, amountNote);


                return Json(new { message = "ok" });
                //return View("Create");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: AmountSitting/Delete/5
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int fromAmount)
        {
            try
            {
                int nu = _sc.TblAmountSitting.Where(d => d.amountFrom == fromAmount).Count();
                for (var i = 0; i < nu; i++)
                {
                    var delAmount = _sc.TblAmountSitting.Where(d => d.amountFrom == fromAmount).FirstOrDefault();

                    _sc.TblAmountSitting.Remove(delAmount);
                    _sc.SaveChanges();

                }

                return Json(new { message = fromAmount });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Data });

            }
        }
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult DeleteStrArr(int fromAmountPrevious)
        {
            try
            {
                int nu = _sc.TblAmountSitting.Where(d => d.amountFrom == fromAmountPrevious).Count();
                for (var i = 0; i < nu; i++)
                {
                    var delAmount = _sc.TblAmountSitting.Where(d => d.amountFrom == fromAmountPrevious).FirstOrDefault();

                    _sc.TblAmountSitting.Remove(delAmount);
                    _sc.SaveChanges();

                }

                return Json(new { message = fromAmountPrevious });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Data });

            }
        }


        // POST: AmountSitting/Delete/5
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