﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCWebAppServierCon.Helpers;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;

namespace MVCWebAppServierCon.Controllers
{
    public class GeneralPrefController : Controller
    {

        private IHostingEnvironment hostingEnviroment { get; }
        private readonly SecondConnClass _context;
        private readonly IConfiguration configuration;


        private string currency_table = "";
        private string connect_with = "";
        SqlConnection connection;

        public GeneralPrefController(SecondConnClass sc, IConfiguration config, IHostingEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
            _context = sc;
            configuration = config;

            string conString = configuration.GetConnectionString("Myconnection");

            connection = new SqlConnection(conString);

            connect_with = _context.TblGeneralPreference.Select(g => g.ConnecWith).FirstOrDefault();
            if (connect_with == Constants.audit)
            {
                currency_table = Constants_Audit.TBLCurrency;
            }
            else
            {
                currency_table = Constants_Finpack.Curr;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Base64String = _context.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _context.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }


        [HttpGet("GeneralPref/")]
        [HttpGet("GeneralPref/index")]
        public async Task<IActionResult> Index()
        {

            return View(await _context.TblGeneralPreference.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Currency = getCurrency(currency_table);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GeneralPreferenceClass model)
        {
            var Gpref = _context.TblGeneralPreference.FirstOrDefault();
            if (Gpref != null)
            {
                if (model.Logo_Files.Count > 0)
                {
                    //save logo
                    var path = await HelperFunctions.save_file(hostingEnviroment, model.Logo_Files[0], "images/Logo");
                    model.Company_Logo = path;


                    //if there is image before 
                    if (Gpref.Company_Logo != null)
                    {
                        HelperFunctions.Delete_file(hostingEnviroment, "images/Logo", Gpref.Company_Logo);
                    }
                }

                Gpref.QouteAmount = model.QouteAmount;
                Gpref.DeductionAmount = model.DeductionAmount;
                Gpref.ConnecWith = model.ConnecWith;
                Gpref.ProjectTable = model.ProjectTable;
                Gpref.ActivitiyTable = model.ActivitiyTable;
                Gpref.link_view_name = model.link_view_name;
                Gpref.Company_Name = model.Company_Name;
                if (model.Logo_Files != null)
                {
                    Gpref.Company_Logo = model.Company_Logo;
                }
                Gpref.Show_Unit = model.Show_Unit;
                Gpref.Show_Doner2 = model.Show_Doner2;
                Gpref.Display_Name_Doner2 = model.Display_Name_Doner2;
                Gpref.Table_Name_Doner2 = model.Table_Name_Doner2;
                Gpref.Show_cost3 = model.Show_cost3;
                Gpref.Display_Name_cost3 = model.Display_Name_cost3;
                Gpref.Table_Name_cost3 = model.Table_Name_cost3;
                Gpref.Show_cost4 = model.Show_cost4;
                Gpref.Display_Name_cost4 = model.Display_Name_cost4;
                Gpref.Table_Name_cost4 = model.Table_Name_cost4;
                Gpref.Show_Order_Type = model.Show_Order_Type;
                Gpref.Display_Name_Project = model.Display_Name_Project;
                Gpref.Display_Name_Activityt = model.Display_Name_Activityt;
                Gpref.Show_ToDate = model.Show_ToDate;
                Gpref.Show_ToEmployee = model.Show_ToEmployee;
                Gpref.Show_Currency_with_item = model.Show_Currency_with_item;
                Gpref.Basic_Currency = model.Basic_Currency;

                _context.SaveChanges();
            }
            else
            {

                if (model.Logo_Files.Count > 0)
                {
                    //save logo
                    var path = await HelperFunctions.save_file(hostingEnviroment, model.Logo_Files[0], "images/Logo");
                    model.Company_Logo = path;
                }
                _context.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model_to_edit = await _context.TblGeneralPreference.FirstOrDefaultAsync(g => g.code == id);

            if (model_to_edit == null)
            {
                return NotFound();
            }


            ViewBag.Currency = getCurrency(currency_table);

            return View(model_to_edit);
        }

        public List<CurrencyViewModel> getCurrency(String tblName)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Code,Name FROM " + tblName + ";", connection);
            var reader = command.ExecuteReader();
            List<CurrencyViewModel> currencies = new List<CurrencyViewModel>();
            while (reader.Read())
            {
                CurrencyViewModel currency = new CurrencyViewModel();
                currency.currencyCode = Int32.Parse(reader.GetValue(0).ToString());
                currency.currencyName = reader.GetValue(1).ToString();

                currencies.Add(currency);
            }
            connection.Close();
            return currencies;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GeneralPreferenceClass model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var previous_model = await _context.TblGeneralPreference.FirstOrDefaultAsync(g => g.code == model.code);

                if (model.Logo_Files != null)
                {
                    if (model.Logo_Files.Count > 0)
                    {
                        //save logo
                        var path = await HelperFunctions.save_file(hostingEnviroment, model.Logo_Files[0], "images/Logo");
                        model.Company_Logo = path;

                        //if there is image before 
                        if (previous_model.Company_Logo != null)
                        {
                            HelperFunctions.Delete_file(hostingEnviroment, "images/Logo", previous_model.Company_Logo);
                        }
                    }
                }

                previous_model.QouteAmount = model.QouteAmount;
                previous_model.DeductionAmount = model.DeductionAmount;
                previous_model.ConnecWith = model.ConnecWith;
                previous_model.ProjectTable = model.ProjectTable;
                previous_model.ActivitiyTable = model.ActivitiyTable;
                previous_model.link_view_name = model.link_view_name;
                previous_model.Company_Name = model.Company_Name;
                if (model.Logo_Files != null)
                {
                    previous_model.Company_Logo = model.Company_Logo;
                }
                previous_model.Show_Unit = model.Show_Unit;
                previous_model.Show_Doner2 = model.Show_Doner2;
                previous_model.Display_Name_Doner2 = model.Display_Name_Doner2;
                previous_model.Table_Name_Doner2 = model.Table_Name_Doner2;
                previous_model.Show_cost3 = model.Show_cost3;
                previous_model.Display_Name_cost3 = model.Display_Name_cost3;
                previous_model.Table_Name_cost3 = model.Table_Name_cost3;
                previous_model.Show_cost4 = model.Show_cost4;
                previous_model.Display_Name_cost4 = model.Display_Name_cost4;
                previous_model.Table_Name_cost4 = model.Table_Name_cost4;
                previous_model.Show_Order_Type = model.Show_Order_Type;
                previous_model.Display_Name_Project = model.Display_Name_Project;
                previous_model.Display_Name_Activityt = model.Display_Name_Activityt;
                previous_model.Show_ToDate = model.Show_ToDate;
                previous_model.Show_ToEmployee = model.Show_ToEmployee;
                previous_model.Show_Currency_with_item = model.Show_Currency_with_item;
                previous_model.Basic_Currency = model.Basic_Currency;

                _context.TblGeneralPreference.Update(previous_model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TblGeneralPreference.Any(g => g.code == model.code))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}