using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCWebAppServierCon.Controllers

{
    public class OrderController : Controller
    {
        private readonly SecondConnClass _sc;
        private readonly ConnnectionStringClass _conns;
        public OrderController(SecondConnClass sc, ConnnectionStringClass conns)
        {
            _sc = sc;
            _conns = conns;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Base64String = _sc.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _sc.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }


        // GET: Order
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Index()
        {
            var result = _sc.TblOrderHeader.ToList();
            foreach (var t in result)
            {
                t.OrderTypeName = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault().orderTypeName;
                //t.ProjectName = _conns.

            }
            return View(result);
        }
        //List<orderTypeHeaderViewModel> testList = new List<orderTypeHeaderViewModel>();
        //List<OrderTypeClass> T1 = _sc.TblOrderType.ToList();
        //List<OrderHeaderClass> T2 = _sc.TblOrderHeader.ToList();

        //int tt = T1.Count();
        //for (int i = 0; i < T1.Count(); i++)
        //{
        //    orderTypeHeaderViewModel testViewModel = new orderTypeHeaderViewModel();
        //    testViewModel.type = T1[i];
        //    if (i < T2.Count())
        //        testViewModel.header = T2[i];
        //    testList.Add(testViewModel);


        //ViewBag.TestView = testViewModel;




        // GET: Order/Details/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Create()
        {
            ViewBag.Order = _sc.TblOrderType.ToList();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Create(OrderTypeClass otc)
        {
            try
            {
                otc.orderTypeCreationDate = DateTime.Now;
                otc.orderTypeUserId = 1;
                _sc.Add(otc);
                _sc.SaveChanges();
                ViewBag.message = "The Record " + otc.orderTypeName + " Is Saved Successfully... !";
                ViewBag.Order = _sc.TblOrderType.ToList();

                return View(otc);

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Edit(int id)
        {
            var res = _sc.TblOrderType.Where(o => o.orderTypeCode == id).FirstOrDefault();
            return View(res);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Edit(int id, OrderTypeClass otc)
        {
            try
            {

                var res = _sc.TblOrderType.Where(o => o.orderTypeCode == id).FirstOrDefault();


                res.orderTypeName = otc.orderTypeName;
                res.orderTypeNote = otc.orderTypeNote;
                res.orderTypeShowFunder = otc.orderTypeShowFunder;
                res.orderTypeShowBudgetLine = otc.orderTypeShowBudgetLine;
                res.orderTypeNumDays = otc.orderTypeNumDays;
                res.orderTypeNumDaysAfter = otc.orderTypeNumDaysAfter;
                res.orderTypeShowTransDate = otc.orderTypeShowTransDate;
                _sc.SaveChanges();

                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int id, OrderTypeClass collection)
        {
            try
            {

                var CheckOrd = _sc.TblOrderHeader.Where(m => m.OrderHeaderOrderTypeCode == id).FirstOrDefault();
                if (CheckOrd != null)
                {
                    ViewBag.message = "Order Type is used";
                    return RedirectToAction(nameof(Create));
                }
                var del = _sc.TblOrderType.Where(i => i.orderTypeCode == id).FirstOrDefault();
                _sc.TblOrderType.Remove(del);
                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return View();
            }
        }


    }
}