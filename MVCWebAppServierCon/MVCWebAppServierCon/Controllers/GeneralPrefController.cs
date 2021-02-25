using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class GeneralPrefController : Controller
    {

        private readonly SecondConnClass _sc;
        public GeneralPrefController(SecondConnClass sc)
        {
            _sc = sc;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(GeneralPreferenceClass model)
        {

             var Gpref = _sc.TblGeneralPreference.FirstOrDefault();
             if (Gpref != null)
            {
                Gpref.QouteAmount = model.QouteAmount;
                Gpref.DeductionAmount = model.DeductionAmount;
                _sc.SaveChanges();
            }
            else
            {
                _sc.Add(model);
                _sc.SaveChanges();
            }
               
            return RedirectToAction(nameof(Create));

            

        }
    }
}