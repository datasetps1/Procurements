﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCWebAppServierCon.Helpers;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;

namespace MVCWebAppServierCon.Controllers
{
    public class HomeController : Controller
    {
        private readonly SecondConnClass _sc;

        private readonly IConfiguration configuration;
        private IHostingEnvironment hostingEnviroment { get; }
        string conString = "";
        SqlConnection connection;
        //private readonly ConnnectionStringClass _sc;

        private string rate_table = "";
        private string currency_table = "";
        private string accounts_table = "";
        private string connect_with = "";


        public HomeController(SecondConnClass sc, IConfiguration config, IHostingEnvironment hostingEnviromen)
        {
            _sc = sc;
            configuration = config;
            conString = configuration.GetConnectionString("Myconnection");
            connection = new SqlConnection(conString);
            this.hostingEnviroment = hostingEnviroment;

            // specify the tables according to genereal preference (finpack or audit)
            connect_with = _sc.TblGeneralPreference.Select(g => g.ConnecWith).FirstOrDefault();
            if (connect_with == Constants.audit)
            {
                rate_table = Constants_Audit.Rate;
                currency_table = Constants_Audit.TBLCurrency;
                accounts_table = Constants_Audit.VAccountSuppliers;
            }
            else
            {
                rate_table = Constants_Finpack.rateTable;
                currency_table = Constants_Finpack.Curr;
                accounts_table = Constants_Finpack.accounts;
            }
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Base64String = _sc.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _sc.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();

            var req = Request;

            int x = 9;
        }

        public async Task<Object> Search_Index(string search_query)
        {
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();
            var getData = new getAuditData();


            int userRank = (int?)user.userTypeCode ?? 0;
            int department = (int?)user.userDepartmentCode ?? 0;

            CompanyDefinition();
            if (userRank == (int)Enums.UserRanks.Employee)
            {

                return RedirectToAction("StuckOrders", "Home");


            }
            if (userRank == 0)
            {
                ModelState.AddModelError("", "There is no data for you");
                return View(orders);
            }
            if (userRank != 0)
            {

                var amount = _sc.TblAmountSitting.Where(a => a.amountStructure.Equals(userRank)).ToList(); // get user amount range
                if (amount.Count != 0)
                {
                    var minAmount = amount.Select(s => s.amountFrom).Min();
                    var maxAmount = amount.Select(s => s.amountTo).Max();

                    if (userRank == (int)Enums.UserRanks.GeneralManager || userRank == (int)Enums.UserRanks.Purchase || userRank == (int)Enums.UserRanks.FinancialDepartment) //|| userRank == (int)Enums.UserRanks.DepartmentManager)
                    {
                        orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderRealTotal * o.OrderHeaderRate > minAmount && o.OrderHeaderRealTotal * o.OrderHeaderRate < maxAmount && o.Freaze == false).ToList();

                    }
                    else if (userRank == (int)Enums.UserRanks.DepartmentManager)
                    {
                        orders = _sc.TblOrderHeader.ToList();
                    }
                    else
                    {
                        orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderRealTotal * o.OrderHeaderRate > minAmount && o.OrderHeaderRealTotal * o.OrderHeaderRate < maxAmount && o.OrderHeaderdepartmentCode == department
                                                       //&& o.OrderHeaderCode == _sc.TblApproval.Where(s=> s.ApprovalHeaderCode == o.OrderHeaderCode && s.ToUser == user.userCode ).Max(r=>r.ApprovalHeaderCode)

                                                       // && _sc.TblApproval.Max(Appr => Appr.ApprovalCode).Where(Appr => Appr.ApprovalHeaderCode == o.OrderHeaderCode  ).
                                                       ).ToList();
                    }

                    foreach (var t in orders)
                    {
                        var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                        ApprovalClass lastApproval = approv.LastOrDefault();
                        if (lastApproval != null && (lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Reject || lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Excuted) || lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Stuck)
                        {
                            continue;
                        }
                        if (lastApproval != null && lastApproval.ToUser == user.userCode)
                        {
                            try
                            {
                                t.OrderTypeName = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault().orderTypeName;
                                t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                                t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                                //t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                                t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                                t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                                t.StatusCode = lastApproval.ApprovalIsApproved;
                                t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                                t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                                t.NotesFromLastAction = lastApproval.ApprovalNote;
                                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                                if (t.SupplierCode != null)
                                {
                                    t.PriceQuoteAmount = getData.GetPriceQouteAmountForSupplier(t.SupplierCode.ToString(), connection);

                                    if (t.PriceQuoteAmount > 0)
                                    {
                                        if ((t.ActualTotalAmount * t.OrderHeaderRate) >= t.PriceQuoteAmount)
                                        {
                                            t.IsNeedPriceQuote = true;
                                        }
                                    }
                                }
                                var dud = _sc.TblGeneralPreference.FirstOrDefault();
                                if ((t.ActualTotalAmount * t.OrderHeaderRate) >= dud.DeductionAmount)
                                {
                                    t.NeedDeductionAtsource = true;
                                }

                                orders2.Add(t);
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message + "At order " + t.OrderHeaderCode);


                            }


                        }
                    }

                    ViewBag.userTypeCode = user.userTypeCode;
                    var RejectedOrders = new List<OrderHeaderClass>();
                    RejectedOrders = orders2.Where(x => x.StatusCode == 3 && x.OrderHeaderdate < DateTime.Now.AddDays(-5)).ToList();

                    orders2 = orders2.Except(RejectedOrders).ToList();

                    //search if the search query is exist 
                    var orders_search_result = new List<OrderHeaderClass>();
                    foreach (var order in orders2)
                    {
                        string Date = null;
                        string expected_date = null;

                        if (order.ExpectedDate != null)
                        {
                            expected_date = order.ExpectedDate.Value.ToString("dd/MM/yyyy");
                        }

                        if (order.ExpectedDate != null)
                        {
                            Date = order.OrderHeaderCreationDate.ToString("dd/MM/yyyy");
                        }

                        search_query = search_query.ToUpper();
                        if ((order.OrderHeaderCode + "").Contains(search_query) ||
                            order.Currency != null && order.Currency.ToUpper().Contains(search_query) ||
                             (order.UserName != null && order.UserName.ToUpper().Contains(search_query)) ||
                            (order.LastUserName != null && order.LastUserName.ToUpper().Contains(search_query)) ||
                             (order.ActualTotalAmount + "").Contains(search_query) ||
                            (order.TotalInbasic + "").Contains(search_query) ||
                             (order.ProjectName != null && order.ProjectName.ToUpper().Contains(search_query)) ||
                            (order.BudgetLine != null && order.BudgetLine.ToUpper().Contains(search_query)) ||
                             (order.OrderTypeName != null && order.OrderTypeName.ToUpper().Contains(search_query)) ||
                             (order.NotesFromLastAction != null && order.NotesFromLastAction.ToUpper().Contains(search_query)) ||
                             (order.SupplierName != null && order.SupplierName.ToUpper().Contains(search_query)) ||
                             (order.StatusName != null && order.StatusName.ToUpper().Contains(search_query)) ||
                             (order.OrderHeaderNote != null && order.OrderHeaderNote.ToUpper().Contains(search_query)) ||
                             (order.waitingUser != null && order.waitingUser.ToUpper().Contains(search_query)) ||
                             (Date != null && Date.Contains(search_query)) ||
                             (expected_date != null && expected_date.Contains(search_query))
                            )
                        {
                            orders_search_result.Add(order);
                        }
                    }

                    return new
                    {
                        result = orders_search_result
                    };


                }
            }
            return new { result = "not found" };
        }

        public async Task<IActionResult> Index(int? pageNumber, int? max_records)
        {/*first select the logged user then get its rank then by the rank get its amount sitting range to use the range the view his orders*/
            //var x = User.Identity.Name;
            //(amountTo >= 600) AND(amountFrom <= 600)
            // var amountsRanks = _sc.TblAmountSitting.Where(a => a.amountTo >= 600 && a.amountFrom <= 600).OrderBy(a => a.amountStructure).ToList(); // get user amount range

            if (pageNumber == null || pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (max_records == null || max_records <= 0)
            {
                max_records = 10;
            }


            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();
            var getData = new getAuditData();


            int userRank = (int?)user.userTypeCode ?? 0;
            int department = (int?)user.userDepartmentCode ?? 0;

            CompanyDefinition();
            if (userRank == (int)Enums.UserRanks.Employee)
            {

                return RedirectToAction("StuckOrders", "Home");


            }
            if (userRank == 0)
            {
                ModelState.AddModelError("", "There is no data for you");
                return View(orders);
            }
            if (userRank != 0)
            {

                var amount = _sc.TblAmountSitting.Where(a => a.amountStructure.Equals(userRank)).ToList(); // get user amount range
                if (amount.Count != 0)
                {
                    var minAmount = amount.Select(s => s.amountFrom).Min();
                    var maxAmount = amount.Select(s => s.amountTo).Max();

                    if (userRank == (int)Enums.UserRanks.GeneralManager || userRank == (int)Enums.UserRanks.Purchase || userRank == (int)Enums.UserRanks.FinancialDepartment) //|| userRank == (int)Enums.UserRanks.DepartmentManager)
                    {
                        orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderRealTotal * o.OrderHeaderRate > minAmount && o.OrderHeaderRealTotal * o.OrderHeaderRate < maxAmount && o.Freaze == false).ToList();

                    }
                    else if (userRank == (int)Enums.UserRanks.DepartmentManager)
                    {
                        orders = _sc.TblOrderHeader.ToList();
                    }
                    else
                    {
                        orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderRealTotal * o.OrderHeaderRate > minAmount && o.OrderHeaderRealTotal * o.OrderHeaderRate < maxAmount && o.OrderHeaderdepartmentCode == department
                                                       //&& o.OrderHeaderCode == _sc.TblApproval.Where(s=> s.ApprovalHeaderCode == o.OrderHeaderCode && s.ToUser == user.userCode ).Max(r=>r.ApprovalHeaderCode)

                                                       // && _sc.TblApproval.Max(Appr => Appr.ApprovalCode).Where(Appr => Appr.ApprovalHeaderCode == o.OrderHeaderCode  ).
                                                       ).ToList();
                    }

                    foreach (var t in orders)
                    {


                        var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                        if (approv.Count == 0)
                        {
                            var ss = 0;
                        }
                        ApprovalClass lastApproval = approv.LastOrDefault();
                        if (lastApproval != null && (lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Reject || lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Excuted) || lastApproval.ApprovalIsApproved == (int)Enums.ApprovalStatus.Stuck)
                        {
                            continue;
                        }
                        if (lastApproval != null && lastApproval.ToUser == user.userCode)
                        {
                            try
                            {
                                var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                                if (show_order_type)
                                {
                                    var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                                    if (order_01 != null)
                                    {
                                        t.OrderTypeName = order_01.orderTypeName;
                                    }
                                }
                                t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                                t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                                t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                                t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                                t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                                t.LastApprovalDate = lastApproval.ApprovalCreationDate;
                                t.StatusCode = lastApproval.ApprovalIsApproved;
                                t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                                t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                                t.NotesFromLastAction = lastApproval.ApprovalNote;
                                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                                if (t.SupplierCode != null)
                                {
                                    t.PriceQuoteAmount = getData.GetPriceQouteAmountForSupplier(t.SupplierCode.ToString(), connection);

                                    if (t.PriceQuoteAmount > 0)
                                    {
                                        if ((t.ActualTotalAmount * t.OrderHeaderRate) >= t.PriceQuoteAmount)
                                        {
                                            t.IsNeedPriceQuote = true;
                                        }
                                    }
                                }
                                var dud = _sc.TblGeneralPreference.FirstOrDefault();
                                if ((t.ActualTotalAmount * t.OrderHeaderRate) >= dud.DeductionAmount)
                                {
                                    t.NeedDeductionAtsource = true;
                                }

                                orders2.Add(t);
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message + "At order " + t.OrderHeaderCode);


                            }


                        }
                    }

                    ViewBag.userTypeCode = user.userTypeCode;
                    var RejectedOrders = new List<OrderHeaderClass>();
                    RejectedOrders = orders2.Where(x => x.StatusCode == 3 && x.OrderHeaderdate < DateTime.Now.AddDays(-5)).ToList();

                    orders2 = orders2.Except(RejectedOrders).ToList();
                    orders2 = orders2.OrderByDescending(o => o.LastApprovalDate).ToList();

                    // for paging -----------------------------------------------------

                    ViewBag.pageNumber = pageNumber;
                    ViewBag.max_records = max_records;
                    var no_of_record = orders2.Count();
                    if (no_of_record % max_records != 0)
                    {
                        ViewBag.no_of_pages = (no_of_record + max_records) / max_records;
                    }
                    else
                    {
                        ViewBag.no_of_pages = (no_of_record) / max_records;
                    }

                    orders2 = orders2.Skip(((int)pageNumber - 1) * (int)max_records).Take((int)max_records).ToList();

                    ViewBag.Orders = orders2;

                }
            }
            return View(orders2);
        }


        [Authorize(Roles = "Admin, EnterStuckOrders")]
        public async Task<Object> Search_StuckOrders(string search_query)
        {

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();

            var getData = new getAuditData();
            var GPref = _sc.TblGeneralPreference.FirstOrDefault();


            orders = _sc.TblOrderHeader.Where(x => x.Freaze == false).ToList();



            foreach (var t in orders)
            {
                var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                ApprovalClass lastApproval = approv.LastOrDefault();
                if (lastApproval != null && lastApproval.ApprovalIsApproved == (int)(int)Enums.ApprovalStatus.Reject)
                {
                    continue;
                }
                if (lastApproval != null && lastApproval.ToUser == user.userCode)
                {
                    var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                    if (show_order_type)
                    {
                        var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                        if (order_01 != null)
                        {
                            t.OrderTypeName = order_01.orderTypeName;
                        }
                    }

                    t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                    t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                    t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                    t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                    t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                    t.StatusCode = lastApproval.ApprovalIsApproved;
                    t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                    t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                    t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                    t.NotesFromLastAction = lastApproval.ApprovalNote;
                    if (t.SupplierCode != null)
                    {
                        //t.PriceQuoteAmount = getData.GetPriceQouteAmountForSupplier(t.SupplierCode.ToString(), connection);

                        //if (t.PriceQuoteAmount > 0)
                        //{
                        //    if ((t.ActualTotalAmount * t.OrderHeaderRate) >= t.PriceQuoteAmount)
                        //    {
                        //        t.IsNeedPriceQuote = true;
                        //    }
                        //}
                    }
                    var dud = _sc.TblGeneralPreference.FirstOrDefault();
                    if ((t.ActualTotalAmount * t.OrderHeaderRate) >= dud.DeductionAmount)
                    {
                        t.NeedDeductionAtsource = true;
                    }

                    orders2.Add(t);
                }
            }

            ViewBag.userTypeCode = user.userTypeCode;
            var RejectedOrders = new List<OrderHeaderClass>();
            RejectedOrders = orders2.Where(x => x.StatusCode == 3 && x.OrderHeaderdate < DateTime.Now.AddDays(-5)).ToList();

            orders2 = orders2.Except(RejectedOrders).ToList();

            //search if the search query is exist 
            var orders_search_result = new List<OrderHeaderClass>();
            foreach (var order in orders2)
            {
                string Date = null;
                string expected_date = null;

                if (order.ExpectedDate != null)
                {
                    expected_date = order.ExpectedDate.Value.ToString("dd/MM/yyyy");
                }

                if (order.ExpectedDate != null)
                {
                    Date = order.OrderHeaderCreationDate.ToString("dd/MM/yyyy");
                }

                search_query = search_query.ToUpper();
                if ((order.OrderHeaderCode + "").Contains(search_query) ||
                    order.Currency.ToUpper().Contains(search_query) ||
                     (order.UserName != null && order.UserName.ToUpper().Contains(search_query)) ||
                    (order.LastUserName != null && order.LastUserName.ToUpper().Contains(search_query)) ||
                     (order.ActualTotalAmount + "").Contains(search_query) ||
                    (order.TotalInbasic + "").Contains(search_query) ||
                     (order.ProjectName != null && order.ProjectName.ToUpper().Contains(search_query)) ||
                    (order.BudgetLine != null && order.BudgetLine.ToUpper().Contains(search_query)) ||
                     (order.OrderTypeName != null && order.OrderTypeName.ToUpper().Contains(search_query)) ||
                     (order.NotesFromLastAction != null && order.NotesFromLastAction.ToUpper().Contains(search_query)) ||
                     (order.SupplierName != null && order.SupplierName.ToUpper().Contains(search_query)) ||
                     (order.StatusName != null && order.StatusName.ToUpper().Contains(search_query)) ||
                     (order.OrderHeaderNote != null && order.OrderHeaderNote.ToUpper().Contains(search_query)) ||
                     (order.waitingUser != null && order.waitingUser.ToUpper().Contains(search_query)) ||
                     (Date != null && Date.Contains(search_query)) ||
                     (expected_date != null && expected_date.Contains(search_query))
                    )
                {
                    orders_search_result.Add(order);
                }
            }

            return new
            {
                result = orders_search_result
            };
        }

        [Authorize(Roles = "Admin, EnterStuckOrders")]
        public async Task<IActionResult> StuckOrders(int? pageNumber, int? max_records)
        {
            if (pageNumber == null || pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (max_records == null || max_records <= 0)
            {
                max_records = 10;
            }

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();

            var getData = new getAuditData();
            var GPref = _sc.TblGeneralPreference.FirstOrDefault();


            orders = _sc.TblOrderHeader.Where(x => x.Freaze == false).ToList();



            foreach (var t in orders)
            {
                if (t.OrderHeaderCode == 726)
                {
                    var ss = 0;
                }
                var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                ApprovalClass lastApproval = approv.LastOrDefault();
                if (lastApproval != null && lastApproval.ApprovalIsApproved == (int)(int)Enums.ApprovalStatus.Reject)
                {
                    continue;
                }
                if (lastApproval != null && lastApproval.ToUser == user.userCode)
                {
                    var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                    if (show_order_type)
                    {
                        var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                        if (order_01 != null)
                        {
                            t.OrderTypeName = order_01.orderTypeName;
                        }
                    }

                    t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                    t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                    t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                    t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                    t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                    t.StatusCode = lastApproval.ApprovalIsApproved;
                    t.LastApprovalDate = lastApproval.ApprovalCreationDate;
                    t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                    t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                    t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                    t.NotesFromLastAction = lastApproval.ApprovalNote;
                    if (t.SupplierCode != null)
                    {
                        //t.PriceQuoteAmount = getData.GetPriceQouteAmountForSupplier(t.SupplierCode.ToString(), connection);

                        //if (t.PriceQuoteAmount > 0)
                        //{
                        //    if ((t.ActualTotalAmount * t.OrderHeaderRate) >= t.PriceQuoteAmount)
                        //    {
                        //        t.IsNeedPriceQuote = true;
                        //    }
                        //}
                    }
                    var dud = _sc.TblGeneralPreference.FirstOrDefault();
                    if ((t.ActualTotalAmount * t.OrderHeaderRate) >= dud.DeductionAmount)
                    {
                        t.NeedDeductionAtsource = true;
                    }

                    orders2.Add(t);
                }
            }

            ViewBag.userTypeCode = user.userTypeCode;
            var RejectedOrders = new List<OrderHeaderClass>();
            RejectedOrders = orders2.Where(x => x.StatusCode == 3 && x.OrderHeaderdate < DateTime.Now.AddDays(-5)).ToList();

            orders2 = orders2.Except(RejectedOrders).ToList();

            orders2 = orders2.OrderByDescending(o => o.LastApprovalDate).ToList();

            // for paging -----------------------------------------------------

            ViewBag.pageNumber = pageNumber;
            ViewBag.max_records = max_records;
            var no_of_record = orders2.Count();
            if (no_of_record % max_records != 0)
            {
                ViewBag.no_of_pages = (no_of_record + max_records) / max_records;
            }
            else
            {
                ViewBag.no_of_pages = (no_of_record) / max_records;
            }

            orders2 = orders2.Skip(((int)pageNumber - 1) * (int)max_records).Take((int)max_records).ToList();

            ViewBag.Orders = orders2;

            if (orders2.Count == 0)
            {
                return RedirectToAction("MyOrders", "Home");
            }
            return View();


        }
        public void CompanyDefinition()
        {
            ViewBag.Base64String = _sc.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _sc.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }

        //public List<LoginViewModel> CompanyDefinition()

        //{// use sql command to make new query to get data from cost table that needed in the order


        //    connection.Open();

        //    SqlCommand command = new SqlCommand("SELECT street2, LogoExt, LogoPath from CompanyDefinition where flag = 1;", connection);
        //    var reader = command.ExecuteReader();
        //    List<LoginViewModel> costLst = new List<LoginViewModel>();
        //    reader.Read();

        //    LoginViewModel costs = new LoginViewModel();
        //    costs.Name = reader.GetValue(0).ToString();
        //    costs.LogoExt = reader.GetValue(1).ToString();
        //    costs.logosting = reader.GetValue(2).ToString();
        //    costs.LogoPath = System.Text.Encoding.UTF8.GetBytes(reader.GetValue(2).ToString());
        //    costs.LogoPath = (byte[])reader["LogoPath"];

        //    //  ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
        //    ViewBag.Base64String = "data:image/jpg;base64," + Convert.ToBase64String(costs.LogoPath);
        //    ViewBag.CompName = costs.Name;
        //    costLst.Add(costs);


        //    // do something with 'value'

        //    connection.Close();
        //    return costLst;
        //}

        public async Task<Object> search_myorders(string query)
        {
            if (query == "")
            {
                return new { result = "no data" };
            }

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();
            var getData = new getAuditData();



            orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderUserId == user.userCode).ToList();


            foreach (var t in orders)
            {
                var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                ApprovalClass lastApproval = approv.LastOrDefault();


                var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                if (show_order_type)
                {
                    var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                    if (order_01 != null)
                    {
                        t.OrderTypeName = order_01.orderTypeName;
                    }
                }
                t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                t.NotesFromLastAction = lastApproval.ApprovalNote;

                orders2.Add(t);
            }
            //filter the list of orders according to the query:

            var orders_result = new List<OrderHeaderClass>();
            foreach (var order in orders2)
            {
                string Date = null;
                string expected_date = null;

                if (order.ExpectedDate != null)
                {
                    expected_date = order.ExpectedDate.Value.ToString("dd/MM/yyyy");
                }

                if (order.ExpectedDate != null)
                {
                    Date = order.OrderHeaderCreationDate.ToString("dd/MM/yyyy");
                }

                query = query.ToUpper();
                if ((order.OrderHeaderCode + "").Contains(query) || order.Currency.ToUpper().Contains(query) ||
                     (order.UserName != null && order.UserName.ToUpper().Contains(query)) ||
                    (order.LastUserName != null && order.LastUserName.ToUpper().Contains(query)) ||
                     (order.ActualTotalAmount + "").Contains(query) ||
                    (order.TotalInbasic + "").Contains(query) ||
                     (order.ProjectName != null && order.ProjectName.ToUpper().Contains(query)) ||
                    (order.BudgetLine != null && order.BudgetLine.ToUpper().Contains(query)) ||
                     (order.OrderTypeName != null && order.OrderTypeName.ToUpper().Contains(query)) ||
                     (order.NotesFromLastAction != null && order.NotesFromLastAction.ToUpper().Contains(query)) ||
                     (order.SupplierName != null && order.SupplierName.ToUpper().Contains(query)) ||
                     (order.StatusName != null && order.StatusName.ToUpper().Contains(query)) ||
                     (order.OrderHeaderNote != null && order.OrderHeaderNote.ToUpper().Contains(query)) ||
                     (order.waitingUser != null && order.waitingUser.ToUpper().Contains(query)) ||
                     (Date != null && Date.Contains(query)) ||
                     (expected_date != null && expected_date.Contains(query))
                    )
                {
                    orders_result.Add(order);
                }
            }

            return new { result = orders_result };
        }

        public async Task<IActionResult> MyOrders(int? pageNumber, int? max_records)
        {
            try
            {
                if (pageNumber == null || pageNumber <= 0)
                {
                    pageNumber = 1;
                }

                if (max_records == null || max_records <= 0)
                {
                    max_records = 10;
                }

                var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
                var orders = new List<OrderHeaderClass>();
                var orders2 = new List<OrderHeaderClass>();
                var getData = new getAuditData();

                ViewBag.pageNumber = pageNumber;
                ViewBag.max_records = max_records;
                var no_of_record = _sc.TblOrderHeader.Where(o => o.OrderHeaderUserId == user.userCode).Count();

                if (no_of_record % max_records != 0)
                {
                    ViewBag.no_of_pages = (no_of_record + max_records) / max_records;
                }
                else
                {
                    ViewBag.no_of_pages = (no_of_record) / max_records;
                }
                orders = _sc.TblOrderHeader.Where(o => o.OrderHeaderUserId == user.userCode).Skip(((int)pageNumber - 1) * (int)max_records).Take((int)max_records).ToList();


                foreach (var t in orders)
                {
                    var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                    ApprovalClass lastApproval = approv.LastOrDefault();


                    var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                    if (show_order_type)
                    {
                        var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                        if (order_01 != null)
                        {
                            t.OrderTypeName = order_01.orderTypeName;
                        }
                    }
                    t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                    t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);
                    t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);
                    t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                    t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                    t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                    t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                    t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                    t.NotesFromLastAction = lastApproval.ApprovalNote;

                    orders2.Add(t);

                }

                ViewBag.Orders = orders2;
                return View(orders2);

            }
            catch (Exception e)
            {
                ViewBag.Error_happend = e.Message;
                return View(new List<OrderHeaderClass>());

            }
        }

        [Authorize(Roles = "Admin, EnterGetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var getData = new getAuditData();
            ViewBag.DepartmentName = _sc.TblDepartment.Where(x => x.departmentGeneralManagerCode == user.userCode || x.departmentManagerCode == user.userCode || x.departmentFinancialCode == user.userCode || x.departmentProcurementSectionCode == user.userCode || x.departmentHeadCode == user.userCode).ToList();
            ViewBag.OrderType = _sc.TblOrderType.ToList();

            ViewBag.Project = getData.getTableData(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), "", connection);
            ViewBag.Employee = _sc.TblUser.OrderBy(x => x.userName).ToList();
            // ViewBag.BudgetLine = getData.getTableData("TBLCost8", connection);
            ViewBag.Supplier = getData.getTableData(accounts_table, "", connection);
            ViewBag.UserDepartmentName = _sc.TblDepartment.Where(x => x.departmentCode == user.userDepartmentCode).Select(u => u.departmentName).FirstOrDefault();
            ViewBag.UserDepartmentCode = user.userDepartmentCode;
            //if(user.userTypeCode == 4 || user.userTypeCode == 5)
            //{
            //    ViewBag.UserDepartmentCodeEnabled = false;
            //}
            //else
            //{
            //    ViewBag.UserDepartmentCodeEnabled = true;
            //}
            ViewBag.UserDepartmentCodeEnabled = true;
            return View();

        }


        [Authorize(Roles = "Admin, EnterMyFollowUp")]
        public async Task<IActionResult> MyFollowUp()
        {
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();
            var getData = new getAuditData();


            var LstApprov = _sc.TblApproval.Where(s => s.ApprovalUserId == user.userUserId).OrderBy(a => a.ApprovalCode).ToList();



            foreach (var a in LstApprov)
            {

                var t = _sc.TblOrderHeader.Where(x => x.OrderHeaderCode == a.ApprovalHeaderCode).FirstOrDefault();
                var show_order_type = _sc.TblGeneralPreference.Select(g => g.Show_Order_Type).FirstOrDefault();
                if (show_order_type)
                {
                    var order_01 = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                    if (order_01 != null)
                    {
                        t.OrderTypeName = order_01.orderTypeName;
                    }
                }
                t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);

                t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);

                t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);

                t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                t.StatusName = GetStatusName(a.ApprovalIsApproved);

                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                t.NotesFromLastAction = a.ApprovalNote;

                orders2.Add(t);
            }

            ViewBag.Orders = orders2;

            return View(orders2);
        }

        //public async Task<IActionResult>

        public async Task<IActionResult> GetAllOrdersBydate(DateTime FromDate, DateTime ToDate, string Project, string BudgetLine, int Department, bool ShowStuckOrdersOnly, string Employee, string WaitingEmployee, string Supplier, bool ShowExecutedOnly, bool ShowUnderExecutedOnly, bool ShowRejectedOnly, double FromAmount, double ToAmount, int FromId, int ToId, int? pageNumber, int? max_records)
        {
            //paging _______________________________________________

            //if (pageNumber == null || pageNumber <= 0)
            //{
            //    pageNumber = 1;
            //}

            //if (max_records == null || max_records <= 0)
            //{
            //    max_records = 10;
            //}
            //ViewBag.pageNumber = pageNumber;
            //ViewBag.max_records = max_records;
            //paging _______________________________________________

            //if (FromDate.ToString() == "01/01/0001 12:00:00 AM")
            //{
            //    FromDate = DateTime.Now.AddMonths(-1);
            //}
            if (ToDate.ToString() == "1/1/0001 12:00:00 AM" || ToDate.ToString() == "01/01/0001 12:00:00 AM")
            {
                ToDate = DateTime.Now;
            }
            ViewBag.date_test = ToDate.ToString();

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var orders = new List<OrderHeaderClass>();
            var orders2 = new List<OrderHeaderClass>();
            var getData = new getAuditData();

            orders = _sc.TblOrderHeader.ToList();
            orders = orders.Where(o => o.OrderHeaderdate >= FromDate && o.OrderHeaderdate <= ToDate).ToList();

            if (Project != null && Project != "0")
            {
                var ProjectCode = getData.getCodeByName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), Project.ToString(), connection);
                orders = orders.Where(o => o.OrderHeaderProjectCode == ProjectCode).ToList();
            }
            if (BudgetLine != null && BudgetLine != "0")
            {
                var BudgetLineCode = getData.getCodeByName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), BudgetLine.ToString(), connection);
                orders = orders.Where(o => o.OrderHeaderBudgetLineCode == BudgetLineCode).ToList();
            }
            if (Department != 0) { orders = orders.Where(o => o.OrderHeaderdepartmentCode == Department).ToList(); }
            if (Department != 0) { orders = orders.Where(o => o.OrderHeaderdepartmentCode == Department).ToList(); }
            if (Employee != "" && Employee != "0" && Employee != null)
            {
                var EmpCode = _sc.TblUser.Where(x => x.userName == Employee).Select(x => x.userCode).FirstOrDefault();
                orders = orders.Where(o => o.OrderHeaderUserId == EmpCode).ToList();

            }
            if (Supplier != "" && Supplier != "0" && Supplier != null)
            {
                var SupplierCode = getData.getCodeByName(accounts_table, Supplier.ToString(), connection);
                orders = orders.Where(o => o.SupplierCode == SupplierCode).ToList();

            }
            if (FromAmount != 0 && ToAmount != 0)
            {
                orders = orders.Where(o => o.ActualTotalAmount * o.OrderHeaderRate >= FromAmount && o.ActualTotalAmount * o.OrderHeaderRate <= ToAmount).ToList();
            }

            if (FromId != -1 || ToId != -1)
            {
                if (FromId == -1)
                {
                    orders = orders.Where(o => o.OrderHeaderCode <= ToId).ToList();
                }
                else if (ToId == -1)
                {
                    orders = orders.Where(o => o.OrderHeaderCode >= FromId).ToList();
                }
                else//FromId != -1 && ToId != -1
                {
                    orders = orders.Where(o => o.OrderHeaderCode >= FromId && o.OrderHeaderCode <= ToId).ToList();
                }
            }
            var orders_withtype_0 = orders.Where(o => o.OrderHeaderOrderTypeCode == 0).ToList();

            foreach (var t in orders)
            {

                var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == t.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                ApprovalClass lastApproval = approv.LastOrDefault();


                var order_type = _sc.TblOrderType.Where(u => u.orderTypeCode == t.OrderHeaderOrderTypeCode).FirstOrDefault();
                if (order_type != null)
                {
                    t.OrderTypeName = order_type.orderTypeName;
                }
                t.ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), t.OrderHeaderProjectCode.ToString(), connection);

                t.BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), t.OrderHeaderBudgetLineCode.ToString(), connection);

                t.Currency = getData.getTblCodeName(currency_table, t.OrderHeaderCurrencey.ToString(), connection);

                t.UserName = _sc.TblUser.Where(u => u.userCode == t.OrderHeaderUserId).FirstOrDefault().userName;
                t.StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                t.StatusCode = lastApproval.ApprovalIsApproved;
                t.LastUserName = _sc.TblUser.Where(u => u.userCode == lastApproval.ApprovalUserId).FirstOrDefault().userName;
                t.waitingUser = _sc.TblUser.Where(u => u.userCode == lastApproval.ToUser).FirstOrDefault().userName;
                t.TotalInbasic = t.ActualTotalAmount * t.OrderHeaderRate;
                t.NotesFromLastAction = lastApproval.ApprovalNote;
                orders2.Add(t);

            }

            if (WaitingEmployee != "" && WaitingEmployee != "0" && WaitingEmployee != null)
            {
                orders2 = orders2.Where(o => o.waitingUser == WaitingEmployee).ToList();

            }
            if (ShowStuckOrdersOnly == true) { orders2 = orders2.Where(o => o.StatusCode == 7).ToList(); }
            if (ShowUnderExecutedOnly == true) { orders2 = orders2.Where(o => o.StatusCode == 8).ToList(); }
            if (ShowExecutedOnly == true) { orders2 = orders2.Where(o => o.StatusCode == 9).ToList(); }
            if (ShowRejectedOnly == true) { orders2 = orders2.Where(o => o.StatusCode == 3).ToList(); }

            var Total = orders2.Sum(o => o.TotalInbasic);
            ViewBag.Total = Math.Round(Total * 100) / 100;
            //var no_of_record = orders2.Count();

            //if (no_of_record % max_records != 0)
            //{
            //    ViewBag.no_of_pages = (no_of_record + max_records) / max_records;
            //}
            //else
            //{
            //    ViewBag.no_of_pages = (no_of_record) / max_records;
            //}

            //orders2 = orders2.Skip(((int)pageNumber - 1) * (int)max_records).Take((int)max_records).ToList();


            ViewBag.Orders = orders2;

            // return View(orders2);
            return PartialView(orders2);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult FreazeOrder(int ApprovalHeaderCode, string Notes)
        {
            var order = _sc.TblOrderHeader.Where(u => u.OrderHeaderCode == ApprovalHeaderCode).FirstOrDefault();

            try
            {
                order.FreazeNote = Notes;
                order.Freaze = true;
                _sc.SaveChanges();
                return new JsonResult(new { success = true, data = "ok" });
            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                //return View("Index");
                return new JsonResult(new { message = "error " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateOrderStatus(int ApprovalHeaderCode, int ApprovalIsApproved, string Notes, string ToEmployeeCode)
        {
            // save approval status in tblapprovals
            //ApprovalIsApproved means :Status
            // Enums enums = new Enums();

            string nextRankUser = "";
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var order = _sc.TblOrderHeader.Where(u => u.OrderHeaderCode == ApprovalHeaderCode).FirstOrDefault();

            if (ToEmployeeCode == null || ToEmployeeCode == "")
            {
                if (ApprovalIsApproved == (int)Enums.ApprovalStatus.Created || ApprovalIsApproved == (int)Enums.ApprovalStatus.Accept || ApprovalIsApproved == (int)Enums.ApprovalStatus.Edit)
                {
                    nextRankUser = GetNextUser(order, user);
                }
                else if (ApprovalIsApproved == (int)Enums.ApprovalStatus.Return)
                {
                    nextRankUser = GetPrevoiusUser(order, user);
                }


                if (ApprovalIsApproved == (int)Enums.ApprovalStatus.Reject || ApprovalIsApproved == (int)Enums.ApprovalStatus.Excuted)
                {
                    nextRankUser = order.OrderHeaderUserId;

                }
                if (ApprovalIsApproved == (int)Enums.ApprovalStatus.UnderExecution)
                {
                    nextRankUser = user.userCode;
                }
            }
            else
            {
                nextRankUser = ToEmployeeCode;
            }

            try
            {

                //get department employee
                ApprovalClass ApprovalModal = new ApprovalClass();

                ApprovalModal.ApprovalUserId = user.userCode;
                ApprovalModal.ApprovalHeaderCode = ApprovalHeaderCode;
                ApprovalModal.ApprovalIsApproved = ApprovalIsApproved;
                ApprovalModal.ApprovalCreationDate = DateTime.Now;
                ApprovalModal.ApprovalNote = Notes;
                ApprovalModal.ToUser = nextRankUser;

                _sc.Add(ApprovalModal);
                _sc.SaveChanges();

                SendEmail(nextRankUser, order, ApprovalIsApproved, Notes);
                return new JsonResult(new { success = true, data = "ok" });


            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                //return View("Index");
                return new JsonResult(new { message = "error " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateOrderStatusAsDocuments(int ApprovalHeaderCode, int ApprovalIsApproved, string Notes)
        {

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();

            try
            {

                //get department employee
                ApprovalClass ApprovalModal = new ApprovalClass();

                ApprovalModal.ApprovalUserId = user.userCode;
                ApprovalModal.ApprovalHeaderCode = ApprovalHeaderCode;
                ApprovalModal.ApprovalIsApproved = (int)Enums.ApprovalStatus.Document;
                ApprovalModal.ApprovalCreationDate = DateTime.Now;
                ApprovalModal.ApprovalNote = Notes;
                ApprovalModal.ToUser = user.userCode; ;

                _sc.Add(ApprovalModal);
                _sc.SaveChanges();


                return new JsonResult(new { success = true, data = "ok" });


            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                //return View("Index");
                return new JsonResult(new { message = "error " + e.Message });
            }
        }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>


        private string GetNextUser(OrderHeaderClass order, UserClass user)
        {
            // get amonts which are in the range of order total ,and get ranks amounts that less than user ranks 
            int nextRank = 0;
            string nextRankUser = "";
            List<AmountSittingClass> amountsRanks = new List<AmountSittingClass>();


            amountsRanks = _sc.TblAmountSitting.Where(a => a.amountTo >= (int)order.OrderHeaderRealTotal * order.OrderHeaderRate && a.amountFrom <= (int)order.OrderHeaderRealTotal * order.OrderHeaderRate && a.amountStructure < user.userTypeCode).OrderBy(a => a.amountStructure).ToList(); // get user amount range
            nextRank = amountsRanks.Select(a => a.amountStructure).LastOrDefault();


            // get next User

            //get user for next rank

            if (nextRank > 3)
            {
                for (int i = amountsRanks.Count() - 1; i >= 0; i--)

                {
                    nextRank = amountsRanks[i].amountStructure;
                    if (amountsRanks[i].amountStructure == (int)Enums.UserRanks.HeadSection)
                    {


                        nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank && u.userDepartmentCode == order.OrderHeaderdepartmentCode).Select(u => u.userCode).FirstOrDefault();
                        //nextRankUser = _sc.TblDepartment.Where(u => u.departmentCode == order.OrderHeaderdepartmentCode).Select(u => u.departmentHeadCode).FirstOrDefault();
                        if (nextRankUser == null)
                        {
                            //this case happen when there is no HeadSection for the deprtment, so it should send to department manager 
                            //(in our case Bashar emp and Nisreen is department manager and there is no section head )
                            nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == 4 && u.userDepartmentCode == order.OrderHeaderdepartmentCode).Select(u => u.userCode).FirstOrDefault();

                        }
                        if (nextRankUser != null)
                        {
                            break;
                        }
                    }
                    if (amountsRanks[i].amountStructure == (int)Enums.UserRanks.DepartmentManager)
                    {
                        nextRankUser = _sc.TblDepartment.Where(u => u.departmentCode == order.OrderHeaderdepartmentCode).Select(u => u.departmentManagerCode).FirstOrDefault();

                        if (nextRankUser != null)
                        {
                            break;
                        }
                    }
                    else
                    {
                        nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank).Select(u => u.userCode).FirstOrDefault();
                    }
                }
            }
            else
            {
                nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank).Select(u => u.userCode).FirstOrDefault();

            }


            /// go to manager if the amount >1000 $
            if (nextRank == (int)Enums.UserRanks.Purchase)
            {
                var ManagerUser = CheckOrderWentToManger(order.OrderHeaderRealTotal, order.OrderHeaderCode, order.OrderHeaderCurrencey);
                if (ManagerUser != "")
                {
                    nextRankUser = ManagerUser;
                    return nextRankUser;
                }


                //  check if the head section make order less than 500 it should go to the department manager not to procurment 
                // like ashraf and  shaher
                //  var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();

                var approval = _sc.TblApproval.Where(u => u.ApprovalHeaderCode == order.OrderHeaderCode).ToList();
                if (approval.Count == 0)
                {
                    if (user.userTypeCode == (int)Enums.UserRanks.HeadSection)
                    {
                        nextRankUser = _sc.TblDepartment.Where(u => u.departmentCode == order.OrderHeaderdepartmentCode).Select(u => u.departmentManagerCode).FirstOrDefault();
                        return nextRankUser;
                    }
                    else if (user.userTypeCode == (int)Enums.UserRanks.DepartmentManager)
                    {
                        nextRankUser = _sc.TblDepartment.Where(u => u.departmentCode == order.OrderHeaderdepartmentCode).Select(u => u.departmentGeneralManagerCode).FirstOrDefault();
                        return nextRankUser;
                    }

                }

            }

            if (nextRank == 0 || nextRankUser == null)
            {
                nextRankUser = order.OrderHeaderUserId;

            }


            return nextRankUser;

        }

        private string GetPrevoiusUser(OrderHeaderClass order, UserClass user)
        {


            string nextRankUser = "";
            var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == order.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
            ApprovalClass lastApproval = approv.LastOrDefault();
            nextRankUser = lastApproval.ApprovalUserId;
            return nextRankUser;
            //// get amonts which are in the range of order total ,and get ranks amounts that less than user ranks 
            //int nextRank = 0;
            //string nextRankUser = "";
            //List<AmountSittingClass> amountsRanks = new List<AmountSittingClass>();




            //amountsRanks = _sc.TblAmountSitting.Where(a => a.amountTo >= (int)order.OrderHeaderRealTotal * order.OrderHeaderRate && a.amountFrom <= (int)order.OrderHeaderRealTotal * order.OrderHeaderRate && a.amountStructure > user.userTypeCode).OrderBy(a => a.amountStructure).ToList(); // get user amount range
            //nextRank = amountsRanks.Select(a => a.amountStructure).FirstOrDefault();

            //// get next User

            ////get user for next rank

            //if (nextRank > 3)
            //{
            //    for (int i = 0; i < amountsRanks.Count(); i++)
            //    {
            //        nextRank = amountsRanks[i].amountStructure;
            //        if (amountsRanks[i].amountStructure == (int)Enums.UserRanks.HeadSection)
            //        {


            //            nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank && u.userDepartmentCode == order.OrderHeaderdepartmentCode).Select(u => u.userCode).FirstOrDefault();
            //            if (nextRankUser != null)
            //            {
            //                break;
            //            }
            //        }
            //        if (amountsRanks[i].amountStructure == (int)Enums.UserRanks.DepartmentManager) // 
            //        {
            //            nextRankUser = _sc.TblDepartment.Where(u => u.departmentCode == order.OrderHeaderdepartmentCode).Select(u => u.departmentManagerCode).FirstOrDefault();

            //            if (nextRankUser != null)
            //            {
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank).Select(u => u.userCode).FirstOrDefault();
            //        }
            //    }
            //}
            //else
            //{
            //    nextRankUser = _sc.TblUser.Where(u => u.userTypeCode == nextRank).Select(u => u.userCode).FirstOrDefault();

            //}


            ///// go to manager if the amount >1000 $
            //if (nextRank == (int)Enums.UserRanks.Purchase)
            //{
            //    var ManagerUser = CheckOrderWentToManger(order.OrderHeaderRealTotal, order.OrderHeaderCode, order.OrderHeaderCurrencey);
            //    if (ManagerUser != "")
            //    {
            //        nextRankUser = ManagerUser;
            //    }
            //}

            //if (nextRank == 0 || nextRankUser == null)
            //{
            //    nextRankUser = order.OrderHeaderUserId;

            //}

            // return nextRankUser;

        }

        private string CheckOrderWentToManger(float Amount, int OrderCode, string orderCurrency)
        {

            // this function return user manager code as a next user to save it in apporval table in case the amount is > 1000$
            // amount should be :Amount * orderRate

            float PriceQouteAmount = 1000;
            int ManagerRank = 2;
            string CurrncyCode = "2";

            // get  today Rate of CurrencyPriceQoute form Audit 
            var AuditData = new getAuditData();
            float Rate = AuditData.GetRate(CurrncyCode, connection, connect_with);

            if (Rate != 0)
            {


                if ((Amount / Rate) > PriceQouteAmount)
                {
                    //Get Manager userCode
                    var ManagerUserId = _sc.TblUser.Where(u => u.userTypeCode == ManagerRank).Select(u => u.userCode).FirstOrDefault();

                    //check if the order went to the manager befor it goes to the purchase department
                    var approval = _sc.TblApproval.Where(u => u.ToUser == ManagerUserId && u.ApprovalHeaderCode == OrderCode).Select(u => u.ApprovalCode).FirstOrDefault();
                    if (approval == 0)
                    {
                        return ManagerUserId;
                    }

                }
            }
            return "";
        }

        private string GetStatusName(int Status)
        {

            switch (Status)
            {
                case -1:
                    return "Document";
                case 0:
                    return "Created";
                case 1:
                    return "Accept";

                case 2:
                    return "Return";

                case 3:
                    return "Reject";


                case 4:
                    return "Edit";


                case 6:
                    return "PriceQuote";


                case 7:
                    return "Stuck";
                case 8:
                    return "UnderExecution";

                case 9:
                    return "Excuted";

                case 10:
                    return "Finished";


            }
            return "";
        }

        private string CheckIfSupplierNeedPriceQoute(float Amount, int OrderCode, string orderCurrency)
        {
            /// this function will read supplier from audit and check the priceQoute amount or we can use CreditAmount as apriceQouteAmt


            return "";
        }

        private JsonResult SendEmail(String ToUser, OrderHeaderClass order, int status, string Notes)
        {
            var CurentUserName = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userName).FirstOrDefault();

            var ToUserEmail = _sc.TblUser.Where(u => u.userCode == ToUser).Select(u => u.userEmail).FirstOrDefault();
            //Get Order - OrginalUser
            var orginaluser = _sc.TblOrderHeader.Where(u => u.OrderHeaderCode == order.OrderHeaderCode).Select(u => u.OrderHeaderUserId).FirstOrDefault();
            var orginalUserEmail = _sc.TblUser.Where(u => u.userCode == orginaluser).Select(u => u.userEmail).FirstOrDefault();

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("DatasetGlob@gmail.com");
            //mail.From = new MailAddress("myasar.fares@gmail.com");

            if (ToUserEmail != null) mail.To.Add(ToUserEmail);
            mail.To.Add(orginalUserEmail);
            //mail.To.Add("myasar.samara@hotmail.com");
            // mail.CC.Add("myasar.samara@hotmail.com");
            mail.IsBodyHtml = true;
            string StatusName = "";
            switch (status)
            {
                case 0:
                    StatusName = " انشاء ";
                    break;
                case 1:
                    StatusName = " الموافقه ";
                    break;

                case 2:
                    StatusName = " الارجاع ";
                    break;
                case 3:
                    StatusName = " الرفض ";
                    break;
            }
            mail.Subject = "Procurmnet System - order Code  : " + order.OrderHeaderCode;
            mail.IsBodyHtml = true;
            string htmlBody;

            htmlBody = @"
            <html lang=""en"">
                <head>    
                    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                    <title>
                        Upcoming topics
                    </title>
                    <style type=""text/css"">

                        .green{background-color: #6B9852;}
                        .divContanerclass{box-shadow: 0 0 10px 0 rgba(0, 0, 0, .1);border-style:solid; border-width:thin ; border:#ea7028}
                        .headerclass{box-shadow: 0 0 10px 0 rgba(0, 0, 0, .1); color:white ;padding:3px; font-size:large; background-color:#ea7028;margin:5px  ;font-family: bold ;}
                        .footer{font-size: x-small ; color: #f7f6f5;  }
                         .hStyle{ margin:10px; }
                        .footer2{box-shadow: 0 0 10px 0 rgba(0, 0, 0, .1);background-color:#ea7028;margin:8px ;font-size: xx-small ; padding:3px ; color:white ;}
                        .btnstyle{background-color:#ea7028 ; color:white ; margin:10px ; padding:2px}
         button:hover {
       
       background-color:silver;
       cursor: pointer;
    }
                       </style>
                </head>
                <body>";
            htmlBody = htmlBody + "<div class='divContanerclass'>";

            htmlBody = htmlBody + "<div  class='headerclass' >Procurment System. ";
            htmlBody = htmlBody + "  </div>";
            if (status == (int)Enums.ApprovalStatus.Created)
            {
                htmlBody = htmlBody + " <h5   class='hStyle'>" + "  لقد تم  " + StatusName + "  طلب رقم : " + order.OrderHeaderCode + "</h5>";

            }
            else
            {
                htmlBody = htmlBody + " <h5   class='hStyle'>" + "  لقد تم  " + StatusName + " على طلب رقم : " + order.OrderHeaderCode + "</h5>";

            }
            htmlBody = htmlBody + "<h5   class='hStyle'>" + CurentUserName + "  من قبل  " + " </h5>";
            htmlBody = htmlBody + "<h5   class='hStyle'> " + "  ملاحظاته  : " + Notes + " </h5>";
            htmlBody = htmlBody + "<a  href= 'http://173.209.48.194:7001/home/index'>" + "Click here" + "</a>";
            htmlBody = htmlBody + "   <p class='footer2'> Dataset Empowering Your Business.</p> ";
            htmlBody = htmlBody + "  </div>";
            htmlBody = htmlBody + "  </body>";
            htmlBody = htmlBody + " </html>";




            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("DatasetGlob@gmail.com", "Dataset@1234");

            SmtpServer.EnableSsl = true;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;

            }

            return new JsonResult(new { success = true, data = "ok" });
        }

        private string createEmailBody(string userName, string message)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/htmlTemplate.html")))
            //{
            //    body = reader.ReadToEnd();
            //}
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{message}", message);
            return body;
        }
        public ActionResult GetSupplier()
        {
            var getData = new getAuditData();
            List<CodeNameModel> lst = new List<CodeNameModel>();
            lst = getData.getTableData(accounts_table, "", connection);
            ViewBag.lstsuppliers = lst;

            return PartialView();
        }

        public ActionResult GetEmployee()
        {

            List<UserClass> lst = new List<UserClass>();
            lst = _sc.TblUser.Where(u => u.userTypeCode == 6 || u.Excutable == true).ToList();

            ViewBag.lstEmps = lst;

            return PartialView();
        }


        [HttpPost]
        public JsonResult TransfeerToEmployee(int ApprovalHeaderCode, int ApprovalIsApproved, string EmpCode, string Notes)
        {

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var order = _sc.TblOrderHeader.Where(u => u.OrderHeaderCode == ApprovalHeaderCode).FirstOrDefault();
            var ToUser = _sc.TblUser.Where(u => u.userName.Equals(EmpCode)).FirstOrDefault();
            try
            {

                //get department employee
                ApprovalClass ApprovalModal = new ApprovalClass();

                ApprovalModal.ApprovalUserId = user.userCode;
                ApprovalModal.ApprovalHeaderCode = ApprovalHeaderCode;
                ApprovalModal.ApprovalIsApproved = ApprovalIsApproved;
                ApprovalModal.ApprovalCreationDate = DateTime.Now;
                ApprovalModal.ApprovalNote = Notes;
                ApprovalModal.ToUser = ToUser.userCode;

                _sc.Add(ApprovalModal);
                _sc.SaveChanges();

                SendEmail(EmpCode, order, 7, Notes);
                return new JsonResult(new { success = true, data = "ok" });


            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                //return View("Index");
                return new JsonResult(new { message = "error " + e.Message });
            }
        }

        [HttpGet]
        public async Task<ContentResult> PrintOrder(int OrderId)
        {
            try
            {
                var getData = new getAuditData();
                var order = _sc.TblOrderHeader.Where(x => x.OrderHeaderCode == OrderId).FirstOrDefault();
                var LstTrans = _sc.TblTransaction.Where(x => x.TransactionOrderHeaderCode == OrderId).ToList();
                var DepartmentName = _sc.TblDepartment.Where(x => x.departmentCode == order.OrderHeaderdepartmentCode).Select(x => x.departmentName).FirstOrDefault();
                var OrderTypeName = _sc.TblOrderType.Where(x => x.orderTypeCode == order.OrderHeaderOrderTypeCode).Select(x => x.orderTypeName).FirstOrDefault();
                var ProjectName = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), order.OrderHeaderProjectCode.ToString(), connection);
                var BudgetLine = getData.getTblCodeName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), order.OrderHeaderBudgetLineCode.ToString(), connection);
                var Currency = getData.getTblCodeName(currency_table, order.OrderHeaderCurrencey.ToString(), connection);
                var UserName = _sc.TblUser.Where(u => u.userCode == order.OrderHeaderUserId).FirstOrDefault().userName;
                var SupplierName = order.SupplierName;
                order.TotalInbasic = order.ActualTotalAmount * order.OrderHeaderRate;
                var table = "";
                var Approvalstable = "";
                var approv = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == order.OrderHeaderCode && s.ApprovalIsApproved != -1).OrderBy(a => a.ApprovalCode).ToList().Distinct().ToList();
                approv = approv.GroupBy(o => new { o.ApprovalUserId, o.ApprovalIsApproved })
                                  .Select(o => o.LastOrDefault()).ToList();
                approv = approv.OrderBy(a => a.ApprovalCode).ToList();
                //var approv2 = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == order.OrderHeaderCode).OrderBy(a => a.ApprovalCode).ToList();
                //var approv3 = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == order.OrderHeaderCode).OrderBy(a => a.ApprovalCode).Distinct().ToList();
                ApprovalClass lastApproval = approv.LastOrDefault();
                var StatusName = GetStatusName(lastApproval.ApprovalIsApproved);
                var LastStatusUser = _sc.TblUser.Where(x => x.userCode == lastApproval.ApprovalUserId).FirstOrDefault();

                var LstApprovals = _sc.TblApproval.Where(s => s.ApprovalHeaderCode == order.OrderHeaderCode && s.ApprovalIsApproved == 1).OrderBy(a => a.ApprovalCode).ToList();
                foreach (var item in LstTrans)
                {
                    var orderdate = "";
                    if (item.TransactionDate.Year > 2009)
                    {
                        orderdate = item.TransactionDate.ToString("dd/MM/yyyy");
                    }
                    table = table + "<tr>" +
                "<td>" + item.TransactionItemCode + "</td>" +
                "<td>" + _sc.TblItem.Where(x => x.itemCode == item.TransactionItemCode).Select(x => x.itemName).FirstOrDefault() + "</td>" +
                "<td>" + item.TransactionQty + "</td>" +
                "<td>" + item.TransactionPrice + "</td>" +
                "<td>" + item.TransactionQty * item.TransactionPrice + "</td>" +
                "<td>" + item.TransactionNote + "</td>" +
                "<td>" + orderdate + "</td>" +
                "</tr>";
                }
                //var Approvals = "Approvals :";
                //    foreach (var item in LstApprovals)
                //{
                //    var approUser = _sc.TblUser.Where(x => x.userCode == item.ApprovalUserId).FirstOrDefault();
                //    Approvals = Approvals +

                //   "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + " : " +  approUser.userName+ "</td>";


                //}

                var Approvals = "Approvals :";
                for (int i = 0; i < approv.Count(); i++)
                {
                    if (approv[i].ApprovalIsApproved == 3)
                    {
                        continue;
                    }
                    var approUser = _sc.TblUser.Where(x => x.userCode == approv[i].ApprovalUserId).FirstOrDefault();
                    // Approvals = Approvals +

                    if (i == 0)
                    {
                        Approvalstable = Approvalstable + "<tr>" +
                        "<td>" + "Prepared by" + "</td>" +
                            "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                             "<td>" + approUser.userName + "</td>" +
                             "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                            "</tr>";
                    }
                    else
                    {
                        if (approv[i].ApprovalIsApproved == 9)
                        {
                            Approvalstable = Approvalstable + "<tr>" +
                           "<td>" + "Executed by" + "</td>" +
                           "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                           "<td>" + approUser.userName + "</td>" +
                            "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                      "</tr>";
                        }
                        else if (approv[i].ApprovalIsApproved == 8)
                        {
                            Approvalstable = Approvalstable + "<tr>" +
                           "<td>" + "Under Execution" + "</td>" +
                           "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                           "<td>" + approUser.userName + "</td>" +
                            "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                      "</tr>";
                        }
                        else if (approv[i].ApprovalIsApproved == 2)
                        {
                            Approvalstable = Approvalstable + "<tr>" +
                               "<td>" + "returned by" + "</td>" +
                               "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                               "<td>" + approUser.userName + "</td>" +
                                "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                            "</tr>";
                        }
                        else if (approv[i].ApprovalIsApproved == 7)
                        {
                            Approvalstable = Approvalstable + "<tr>" +
                               "<td>" + "Stuck " + "</td>" +
                               "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                               "<td>" + approUser.userName + "</td>" +
                                "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                            "</tr>";
                        }


                        else
                        {
                            Approvalstable = Approvalstable + "<tr>" +
                                "<td>" + "Approved by" + "</td>" +
                                "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
                                "<td>" + approUser.userName + "</td>" +
                                 "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
                           "</tr>";
                        }
                    }



                }

                // order.ExpectedDate != null ? order.ExpectedDate.Value.ToString("dd/MM/yyyy") : "-"
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html>" +

                      "<style type = 'text/css'>" +
                ".div{" +
                        "display:inline-flex; color: green;" +
                "}" +
                ".fnt{" +
                        " font-size:small ;" +
                "}" +
                ".p{" +
                        " width:max-content ;" +
                       "padding:2px;" +
                "}" +
                ".th{" +
                        "font-size:small ;height:5px ;background-color:#dfdfdf ;" +
                "}" +
               " </style> " +



                    "<body>" +

                    "<br />" +

                 //"Order  " + OrderId +
                 "<div class='container' id='invoice' style='padding:2px;'>" +
                "<div class='row'>" +
                 "<div class='col-md-1'></div>" +
                  "<div class='col-md-10 border'>" +


                 " <div class='row'>" +
                   "<div class='col-md-12  text-center text-primary'>" +
                     "  <h2>Order " + OrderId + "</h2>" +
                    "</div>" +
                 "</div>" +


                "<div class='row'>" +
                "<div class='col-md-8'>" +
                  "<div class='row'><p class='p'><strong>Employee: </strong><strong> " + UserName + "</strong> </p></div>" +
                 "<div class='row'><p class='p'><strong>Department Name: </strong> <strong>" + DepartmentName + " </strong></p></div>" +
               " </div>" +


                "<div class='col-md-4 text-right'>" +
                   "<div class='row'><p class='p'><strong>Order date: </strong> <strong>" + order.OrderHeaderdate.ToString("dd/MM/yyyy") + " </strong></p> </div > " +
                  "<div class='row'><p class='p'> <strong>Expected Date : </strong> <strong>" + order.OrderHeaderdate.ToString("dd/MM/yyyy") + "</strong></p></div>" +

               " </div>" +
              "</div>" + //row1

              "<div class='row'>" +
                "<div class='col-md-7'>" +
                   "<div class='row'><p class='p'><strong>Project Name: </strong> <strong>" + ProjectName + "</strong> </p></div> " +
                  "<div class='row'><p class='p'><strong>Budget Line: </strong> <strong>" + BudgetLine + "</strong></p></div> " +
               " </div>" +


                "<div class='col-md-5 text-right '>" +
                  "<div class='row'><p class='p'><strong>Order Type: </strong> <strong>" + OrderTypeName + "</strong></p></div> " +
                   "<div class='row'><p class='p'><strong>Supplier: </strong> <strong>" + SupplierName + "</strong></p></div> " +
               " </div>" +
              "</div>" + //row2

                "<div class='row'>" +
                "<div class='col-md-7'>" +
                   "<div class='row'><p class='p'><strong>General Notes: </strong> <strong>" + order.OrderHeaderNote + " </strong></p></div> " +

               " </div>" +
                "</div>" + //row3

               "<div class='row>" +
                "<div class='col-md-12 well invoice-body'>" +
                  "<table class='table table-bordered'>" +
                   //" <table id='tbl2' class='table table-striped responsive-table text-center table-hover table-bordered'>"+
                   "<thead style = 'background-color:#46688a;font-size:smaller ;' >" +
                   " <tr> " +
                    "<th class='th'>" + "Item" + "</th>" +
                    "<th class='th'>" + "Name" + "</th>" +
                    "<th class='th'>" + "Qty" + "</th>" +
                    "<th class='th' >" + "Price" + "</th>" +
                    "<th class='th'>" + "Total" + "</th>" +
                    "<th class='th'>" + "Notes" + "</th>" +
                    "<th class='th'>" + "Date" + "</th>" +
                    " </tr ></thead>" +
                    table +

                   "</table>" +
                " </div>" +



                "<div class='row'>" +
                 "<div class='col-md-1 text-left'>status</div>" +


                   "<div class='col-md-1 text-left'>" + StatusName + "</div>" +

                        "<div class='col-md-2 text-left'>" + "By : " + LastStatusUser.userName + "</div>" +


                 "<div class='col-md-5 text-right'>Total</div>" +

                   "<div class='col-md-2 text-right'>" + order.ActualTotalAmount + "</div>" +
                   "<div class='col-md-1 text-right'>" + Currency + "</div>" +

               "</div>" +
                      "</br>" +


                 "<div class='row'>" +
                 "<div class='col-md-1 text-left'> </div>" +


                   "<div class='col-md-1 text-left'></div>" +


                 "<div class='col-md-7 text-right'>Total In Basic Currency</div>" +

                   "<div class='col-md-2 text-right'>" + order.TotalInbasic + "</div>" +
                   "<div class='col-md-1 text-right'>" + "NIS" + "</div>" +

               "</div>" +
               "</br>" +
                 "<div class='row>" +
                "<div class='col-md-12 well invoice-body'>" +
                  "<table class='table table-bordered'>" +
                    //" <table id='tbl2' class='table table-striped responsive-table text-center table-hover table-bordered'>"+

                    Approvalstable +

                   "</table>" +
                " </div>" +

             "</div>" +


               "</div>" +



            "</div>" +

                    "</body ></html > "
                };

            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                //var x = ex.
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                ViewBag.Error = ex.Message + " / in line = " + line;
                return null;
            }
        }

    }
}
public class Enums
{

    public enum ApprovalStatus
    {
        //  Canceled = -2,
        Document = -1,
        Created = 0,
        Accept = 1,
        Return = 2,
        Reject = 3,
        Edit = 4,
        PriceQuote = 6,
        Stuck = 7,//Transfeer
        UnderExecution = 8,
        Excuted = 9,
        Finished = 10,
    }

    public enum UserRanks
    {
        Purchase = 1,
        GeneralManager = 2,
        FinancialDepartment = 3,
        DepartmentManager = 4,
        HeadSection = 5,
        Employee = 6

    }

}