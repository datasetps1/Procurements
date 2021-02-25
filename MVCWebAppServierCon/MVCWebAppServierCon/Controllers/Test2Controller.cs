//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using MVCWebAppServierCon.Models;
//namespace MVCWebAppServierCon.Controllers
//{


//    namespace MVCWebAppServierCon.Controllers
//    {
//        public class Test2Controller : Controller
//        {
//            private readonly SecondConnClass _sc;

//            public Test2Controller(SecondConnClass sc)
//            {
//                _sc = sc;
//            }
//            public IActionResult Index()
//            {
//                var res = _sc.TblTest2.ToList();
//                return View(res);

//            }


//            //Get: Test2/Create
//            public ActionResult Create()
//            {
//                var mess = TempData["ErrorMessage"] as String;
//                if (mess != null && mess.Equals("error"))
//                {
//                    ModelState.AddModelError("", "You Need To add the test before");
//                }
//                ViewBag.Tests = _sc.TblTest2.ToList();
//                return View();
//            }

//            // POST: Test2/Create
//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public ActionResult Create(Test2Class tc)
//            {
//                try
//                {
//                    // TODO: Add insert logic here

//                    tc.creationDate = DateTime.Now;
//                    _sc.Add(tc);
//                    _sc.SaveChanges();

//                    ViewBag.Tests = _sc.TblTest2.ToList();
//                    ViewBag.message = "The Record " + tc.testName + " has been tested";

//                    return View(tc);
//                }
//                catch
//                {
//                    return View(tc);
//                }
//            }

//            // GET: Test2/Edit/5
//            public ActionResult Edit(int id)
//            {
//                var res = _sc.TblTest2.Where(i => i.testCode == id).FirstOrDefault();
//                return View(res);
//            }

//            // POST: Test2/Edit/5
//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public ActionResult Edit(int id, Test2Class ic)
//            {
//                try
//                {
//                    var res = _sc.TblTest2.Where(i => i.testCode == id).FirstOrDefault();
//                    res.testName = ic.testName;

//                    _sc.SaveChanges();
//                    // TODO: Add update logic here

//                    return RedirectToAction(nameof(Create));
//                }
//                catch
//                {
//                    return View();
//                }
//            }

//            // GET: Item/Delete/5
//            public ActionResult Delete(int id)
//            {
//                var del = _sc.TblTest2.Where(i => i.testCode == id).FirstOrDefault();
//                /*_sc.TblItem.Remove(del);
//                _sc.SaveChanges();*/
//                return View(del);
//            }

//            // POST: Test/Delete/5
//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public ActionResult Delete(int id, Test2Class collection)
//            {
//                try
//                {

//                    var del = _sc.TblTest2.Where(i => i.testCode == id).FirstOrDefault();
//                    _sc.TblTest.Remove(del);
//                    _sc.SaveChanges();

//                    return RedirectToAction(nameof(Create));
//                }
//                catch
//                {
//                    return View();
//                }
//            }


//        }
//    }
//}
