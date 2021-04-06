using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCWebAppServierCon.Helpers;
using MVCWebAppServierCon.Models;
using MVCWebAppServierCon.ViewModels;

namespace MVCWebAppServierCon.Controllers
{
    public class CompleteActivitiesController : Controller
    {


        private IHostingEnvironment hostingEnviroment { get; }
        private readonly SecondConnClass _context;
        SqlConnection connection;

        private string rate_table = "";
        private string currency_table = "";
        private string accounts_table = "";
        private string connect_with = "";

        public CompleteActivitiesController(SecondConnClass context, IConfiguration config, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            this.hostingEnviroment = hostingEnviroment; 
            var conString = config.GetConnectionString("Myconnection");
            connection = new SqlConnection(conString);

            // specify the tables according to genereal preference (finpack or audit)
            connect_with = _context.TblGeneralPreference.Select(g => g.ConnecWith).FirstOrDefault();
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

            ViewBag.Base64String = _context.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _context.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }

        public void CompanyDefinition()
        {
            ViewBag.Base64String = _context.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _context.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }
        // GET: CompleteActivities/Create
        public async Task<IActionResult> Create(int? code)
        {

            CompanyDefinition();

            ViewBag.OrderHeader = await _context.TblOrderHeader.FirstOrDefaultAsync(o => o.OrderHeaderCode == code);

            ViewBag.WebRootPath = hostingEnviroment.WebRootPath;


            var getData = new getAuditData();
            ViewBag.supplier = getData.getTableData(accounts_table, "", connection);

            var CompleteActivity = await _context.CompleteActivity.FirstOrDefaultAsync(c => c.OrderCode == code);
            if (CompleteActivity != null)
            {
                ViewBag.update_status = true;
                ViewBag.CompleteActivityFiles = await _context.CompleteActivityFiles.Where(c => c.CompleteActivityId == CompleteActivity.Id).ToListAsync();
                ViewBag.CompleteActivityOffered = await _context.CompleteActivityOffered.Where(c => c.CompleteActivityId == CompleteActivity.Id).ToListAsync();
            }



            return View(CompleteActivity);
        }

        // POST: CompleteActivities/Create
        [HttpPost]
        public ActionResult Create(string ActivityDate, string Date, CompleteActivity completeActivity, List<CompleteActivityOffered> offered_list, List<string> paths_array)
        {
            //add data to database
            var date_array = Date.Split('-');
            var ActivityDate_array = ActivityDate.Split('-');
            completeActivity.Date = new DateTime(int.Parse(date_array[0]), int.Parse(date_array[1]), int.Parse(date_array[2]));
            completeActivity.ActivityDate = new DateTime(int.Parse(ActivityDate_array[0]), int.Parse(ActivityDate_array[1]), int.Parse(ActivityDate_array[2]));

            //prepare files
            var activityFiles = new List<CompleteActivityFiles>();
            foreach (var current in paths_array)
            {
                activityFiles.Add(new CompleteActivityFiles()
                {
                    File_path = current
                });
            }

            completeActivity.activityFiles = activityFiles;

            _context.CompleteActivity.Add(completeActivity);
            _context.SaveChanges();

            return Json(new { direct_urk = Url.Action("StuckOrders", "home") });
        }

        // POST: CompleteActivities/CreateFiles
        [HttpPost]
        public async Task<IActionResult> CreateFiles(IFormCollection request)
        {
            string[] paths_array = new string[request.Files.Count];

            //Helpers.HelperFunctions.save_file();
            int x = 0;

            for (int i = 0; i < request.Files.Count; i++)
            {
                var file_path = await Helpers.HelperFunctions.save_file(hostingEnviroment, request.Files[i], "images/complete_activity");
                paths_array[i] = file_path;
            }


            return Json(new { paths_array = paths_array });

        }

        // POST: CompleteActivities/update
        [HttpPost]
        public ActionResult Update(string ActivityDate, string Date, CompleteActivity completeActivity, List<CompleteActivityOffered> offered_list, List<string> paths_array , List<int> deleted_files_list, List<CompleteActivityOffered> deleted_offered_list)
        {

            var date_array = Date.Split('-');
            var ActivityDate_array = ActivityDate.Split('-');
            completeActivity.Date = new DateTime(int.Parse(date_array[0]), int.Parse(date_array[1]), int.Parse(date_array[2]));
            completeActivity.ActivityDate = new DateTime(int.Parse(ActivityDate_array[0]), int.Parse(ActivityDate_array[1]), int.Parse(ActivityDate_array[2]));

            //add offer list
            for (int i=0; i < offered_list.Count; i++){
                offered_list[i].CompleteActivityId = completeActivity.Id;
            }



            _context.CompleteActivityOffered.AddRange(offered_list);

            //delete offer list 
            _context.CompleteActivityOffered.RemoveRange(deleted_offered_list);

            //add new files
            //prepare files
            var activityFiles = new List<CompleteActivityFiles>();
            foreach (var current in paths_array)
            {
                activityFiles.Add(new CompleteActivityFiles()
                {
                    File_path = current,
                    CompleteActivityId = completeActivity.Id
                });
            }

            _context.CompleteActivityFiles.AddRange(activityFiles);

            //delete files...

            foreach(var current in deleted_files_list)
            {
                var current_activity_File = _context.CompleteActivityFiles.FirstOrDefault(f => f.Id == current);
                if (current_activity_File == null)
                {
                    return NotFound();
                }

                Helpers.HelperFunctions.Delete_file(hostingEnviroment, "images/complete_activity", current_activity_File.File_path);

                int x = 0;
                _context.CompleteActivityFiles.Remove(current_activity_File);

            }

            _context.CompleteActivity.Update(completeActivity);
            _context.SaveChanges();

            return Json(new { direct_urk = Url.Action("StuckOrders", "home") });
        }

        //public ActionResult GetSupplier()
        //{
        //    var getData = new getAuditData();
        //    List<CodeNameModel> lst = new List<CodeNameModel>();
        //    lst = getData.getTableData(accounts_table, "", connection);
        //    ViewBag.lstsuppliers = lst;

        //    return PartialView();
        //}


        private bool CompleteActivityExists(int id)
        {
            return _context.CompleteActivity.Any(e => e.Id == id);
        }

        public async Task<ContentResult> Print(int OrderId)
        {
            
            var table = "";
            return null;
   //         foreach (var item in [])
   //         {
   //             var orderdate = "";
   //             if (item.TransactionDate.Year > 2009)
   //             {
   //                 orderdate = item.TransactionDate.ToString("dd/MM/yyyy");
   //             }
   //             table = table + "<tr>" +
   //         "<td>" + item.TransactionItemCode + "</td>" +
   //         "<td>" + _sc.TblItem.Where(x => x.itemCode == item.TransactionItemCode).Select(x => x.itemName).FirstOrDefault() + "</td>" +
   //         "<td>" + item.TransactionQty + "</td>" +
   //         "<td>" + item.TransactionPrice + "</td>" +
   //         "<td>" + item.TransactionQty * item.TransactionPrice + "</td>" +
   //         "<td>" + item.TransactionNote + "</td>" +
   //         "<td>" + orderdate + "</td>" +
   //         "</tr>";
   //         }
   //         //var Approvals = "Approvals :";
   //         //    foreach (var item in LstApprovals)
   //         //{
   //         //    var approUser = _sc.TblUser.Where(x => x.userCode == item.ApprovalUserId).FirstOrDefault();
   //         //    Approvals = Approvals +

   //         //   "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + " : " +  approUser.userName+ "</td>";


   //         //}

   //         var Approvals = "Approvals :";
   //         for (int i = 0; i < approv.Count(); i++)
   //         {
   //             if (approv[i].ApprovalIsApproved == 3)
   //             {
   //                 continue;
   //             }
   //             var approUser = _sc.TblUser.Where(x => x.userCode == approv[i].ApprovalUserId).FirstOrDefault();
   //             // Approvals = Approvals +

   //             if (i == 0)
   //             {
   //                 Approvalstable = Approvalstable + "<tr>" +
   //                 "<td>" + "Prepared by" + "</td>" +
   //                     "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
   //                      "<td>" + approUser.userName + "</td>" +
   //                      "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
   //                     "</tr>";
   //             }
   //             else
   //             {
   //                 if (approv[i].ApprovalIsApproved == 9)
   //                 {
   //                     Approvalstable = Approvalstable + "<tr>" +
   //                    "<td>" + "Executed by" + "</td>" +
   //                    "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
   //                    "<td>" + approUser.userName + "</td>" +
   //                     "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
   //               "</tr>";
   //                 }
   //                 else if (approv[i].ApprovalIsApproved == 8)
   //                 {
   //                     Approvalstable = Approvalstable + "<tr>" +
   //                    "<td>" + "Under Execution" + "</td>" +
   //                    "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
   //                    "<td>" + approUser.userName + "</td>" +
   //                     "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
   //               "</tr>";
   //                 }


   //                 else
   //                 {


   //                     Approvalstable = Approvalstable + "<tr>" +
   //                         "<td>" + "Approved by" + "</td>" +
   //                         "<td>" + _sc.TblStructure.Where(x => x.structureRank == approUser.userTypeCode).Select(x => x.structureName).FirstOrDefault() + "</td>" +
   //                         "<td>" + approUser.userName + "</td>" +
   //                          "<td>" + approv[i].ApprovalCreationDate.ToString("dd/MM/yyyy") + "</td>" +
   //                    "</tr>";
   //                 }
   //             }



   //         }

   //         // order.ExpectedDate != null ? order.ExpectedDate.Value.ToString("dd/MM/yyyy") : "-"
   //         return new ContentResult
   //         {
   //             ContentType = "text/html",
   //             StatusCode = (int)HttpStatusCode.OK,
   //             Content = "<html>" +

   //               "<style type = 'text/css'>" +
   //         ".div{" +
   //                 "display:inline-flex; color: green;" +
   //         "}" +
   //         ".fnt{" +
   //                 " font-size:small ;" +
   //         "}" +
   //         ".p{" +
   //                 " width:max-content ;" +
   //                "padding:2px;" +
   //         "}" +
   //         ".th{" +
   //                 "font-size:small ;height:5px ;background-color:#dfdfdf ;" +
   //         "}" +
   //        " </style> " +



   //             "<body>" +

   //             "<br />" +

   //          //"Order  " + OrderId +
   //          "<div class='container' id='invoice' style='padding:2px;'>" +
   //         "<div class='row'>" +
   //          "<div class='col-md-1'></div>" +
   //           "<div class='col-md-10 border'>" +


   //          " <div class='row'>" +
   //            "<div class='col-md-12  text-center text-primary'>" +
   //              "  <h2>Order " + OrderId + "</h2>" +
   //             "</div>" +
   //          "</div>" +


   //         "<div class='row'>" +
   //         "<div class='col-md-8'>" +
   //           "<div class='row'><p class='p'><strong>Employee: </strong><strong> " + UserName + "</strong> </p></div>" +
   //          "<div class='row'><p class='p'><strong>Department Name: </strong> <strong>" + DepartmentName + " </strong></p></div>" +
   //        " </div>" +


   //         "<div class='col-md-4 text-right'>" +
   //            "<div class='row'><p class='p'><strong>Order date: </strong> <strong>" + order.OrderHeaderdate.ToString("dd/MM/yyyy") + " </strong></p> </div > " +
   //           "<div class='row'><p class='p'> <strong>Expected Date : </strong> <strong>" + order.OrderHeaderdate.ToString("dd/MM/yyyy") + "</strong></p></div>" +

   //        " </div>" +
   //       "</div>" + //row1

   //       "<div class='row'>" +
   //         "<div class='col-md-7'>" +
   //            "<div class='row'><p class='p'><strong>Project Name: </strong> <strong>" + ProjectName + "</strong> </p></div> " +
   //           "<div class='row'><p class='p'><strong>Budget Line: </strong> <strong>" + BudgetLine + "</strong></p></div> " +
   //        " </div>" +


   //         "<div class='col-md-5 text-right '>" +
   //           "<div class='row'><p class='p'><strong>Order Type: </strong> <strong>" + OrderTypeName + "</strong></p></div> " +
   //            "<div class='row'><p class='p'><strong>Supplier: </strong> <strong>" + SupplierName + "</strong></p></div> " +
   //        " </div>" +
   //       "</div>" + //row2

   //         "<div class='row'>" +
   //         "<div class='col-md-7'>" +
   //            "<div class='row'><p class='p'><strong>General Notes: </strong> <strong>" + order.OrderHeaderNote + " </strong></p></div> " +

   //        " </div>" +
   //         "</div>" + //row3

   //        "<div class='row>" +
   //         "<div class='col-md-12 well invoice-body'>" +
   //           "<table class='table table-bordered'>" +
   //             //" <table id='tbl2' class='table table-striped responsive-table text-center table-hover table-bordered'>"+
   //            "<thead style = 'background-color:#46688a;font-size:smaller ;' >" +
   //            " <tr> " +
   //             "<th class='th'>" + "Item" + "</th>" +
   //             "<th class='th'>" + "Name" + "</th>" +
   //             "<th class='th'>" + "Qty" + "</th>" +
   //             "<th class='th' >" + "Price" + "</th>" +
   //             "<th class='th'>" + "Total" + "</th>" +
   //             "<th class='th'>" + "Notes" + "</th>" +
   //             "<th class='th'>" + "Date" + "</th>" +
   //             " </tr ></thead>" +
   //             table +

   //            "</table>" +
   //         " </div>" +



   //         "<div class='row'>" +
   //          "<div class='col-md-1 text-left'>status</div>" +


   //            "<div class='col-md-1 text-left'>" + StatusName + "</div>" +

   //                 "<div class='col-md-2 text-left'>" + "By : " + LastStatusUser.userName + "</div>" +


   //          "<div class='col-md-5 text-right'>Total</div>" +

   //            "<div class='col-md-2 text-right'>" + order.ActualTotalAmount + "</div>" +
   //            "<div class='col-md-1 text-right'>" + Currency + "</div>" +

   //        "</div>" +
   //               "</br>" +


   //          "<div class='row'>" +
   //          "<div class='col-md-1 text-left'> </div>" +


   //            "<div class='col-md-1 text-left'></div>" +


   //          "<div class='col-md-7 text-right'>Total In Basic Currency</div>" +

   //            "<div class='col-md-2 text-right'>" + order.TotalInbasic + "</div>" +
   //            "<div class='col-md-1 text-right'>" + "NIS" + "</div>" +

   //        "</div>" +
   //        "</br>" +
   //          "<div class='row>" +
   //         "<div class='col-md-12 well invoice-body'>" +
   //           "<table class='table table-bordered'>" +
   //             //" <table id='tbl2' class='table table-striped responsive-table text-center table-hover table-bordered'>"+

   //             Approvalstable +

   //            "</table>" +
   //         " </div>" +

   //      "</div>" +


   //        "</div>" +



   //"</div>" +

   //             "</body ></html > "
   //         };
        }
    }
}
