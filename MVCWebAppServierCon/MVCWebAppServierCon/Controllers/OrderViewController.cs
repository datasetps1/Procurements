using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MVCWebAppServierCon.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MVCWebAppServierCon.Helpers;

namespace MVCWebAppServierCon.Controllers
{

    public class OrderViewController : Controller
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

        public OrderViewController(SecondConnClass sc, IConfiguration config, IHostingEnvironment hostingEnviroment)
        {

            _sc = sc;
            var GPref = _sc.TblGeneralPreference.FirstOrDefault();
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

        public IActionResult Index()
        {
            List<OrderViewModel> orderViewList = new List<OrderViewModel>();
            List<OrderHeaderClass> O1 = _sc.TblOrderHeader.ToList();
            List<TransactionClass> O2 = _sc.TblTransaction.ToList();
            int tt = O1.Count();
            for (int i = 0; i < O1.Count(); i++)
            {
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.headerClass = O1[i];
                if (i < O2.Count())
                    orderViewModel.transClass = O2.ToList();
                orderViewList.Add(orderViewModel);
            }

            //ViewBag.TestView = testViewModel;

            ViewBag.ListView = orderViewList;

            return View();
        }

        // get cost from another database 
        public List<CostsViewModel> projLoad(String tblName)
        {// use sql command to make new query to get data from cost table that needed in the order
            connection.Open();

            SqlCommand command = new SqlCommand();
            if (connect_with == Constants.audit)
            {
                command = new SqlCommand("SELECT Code,Name,isnull(Budget,0) as Budget  FROM " + tblName + " Where freeze=0;", connection);
            }
            else if (connect_with == Constants.finpack)
            {
                command = new SqlCommand("SELECT Code,Name  FROM " + tblName + " Where Status=1;", connection);
            }


            var reader = command.ExecuteReader();
            List<CostsViewModel> costLst = new List<CostsViewModel>();
            while (reader.Read())
            {
                CostsViewModel costs = new CostsViewModel();
                costs.costCode = reader.GetValue(0).ToString();
                costs.costName = reader.GetValue(1).ToString();
                //costs.costBudget = float.Parse(reader.GetValue(2).ToString());
                costLst.Add(costs);

                // do something with 'value'
            }
            connection.Close();
            return costLst;
        }

        public String projName(String tblName, string code)
        {// use sql command to make new query to get data from cost table that needed in the order
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Name FROM " + tblName + " Where Code= '" + code + "' ;", connection);
            var reader = command.ExecuteReader();
            String name = "";
            while (reader.Read())
            {
                name = reader.GetValue(0).ToString();

                // do something with 'value'
            }
            connection.Close();
            return name;
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

        public ActionResult GetBudgetLineByFunder(string Fundercode)
        {

            // SqlCommand command = new SqlCommand("SELECT code,name FROM  VRelationalBudgetLineWithFunder WHERE (FirstCostCenterCode = '" + Fundercode + "' );", connection);

            var getData = new getAuditData();
            List<CodeNameModel> lst = new List<CodeNameModel>();
            lst = getData.getTableData("VRelationalBudgetLineWithFunder", "  WHERE (FirstCostCenterCode = '" + Fundercode + "' ) ", connection);
            ViewBag.lst = lst;

            return PartialView();

        }

        public ActionResult GetBudgetLineByFunderData(string Fundercode)
        {

            // SqlCommand command = new SqlCommand("SELECT code,name FROM  VRelationalBudgetLineWithFunder WHERE (FirstCostCenterCode = '" + Fundercode + "' );", connection);

            var getData = new getAuditData();
            List<CodeNameModel> lst = new List<CodeNameModel>();
            lst = getData.getTableData("VRelationalBudgetLineWithFunder", "  WHERE (FirstCostCenterCode = '" + Fundercode + "' ) ", connection);
            ViewBag.lst = lst;

            return Json(new { data = lst });

        }
        public ActionResult getRate(int rateId)
        {
            connection.Open();
            var date = DateTime.Now.ToString("yyyy-MM-dd");  //'2020-02-24'

            SqlCommand command = new SqlCommand();
            if (connect_with == Constants.audit)
            {
                command = new SqlCommand("SELECT Rate FROM " + rate_table + " WHERE (Code = '" + rateId + "' and RateDate = '" + date + "');", connection); // + "' and RateDate = '" + DateTime.Today.Date.ToString("yyyy-MM-dd")            }
            }
            else if (connect_with == Constants.finpack)
            {
                command = new SqlCommand("SELECT Rate FROM " + rate_table + " WHERE (Code = '" + rateId + "' and RDate = '" + date + "');", connection); // + "' and RateDate = '" + DateTime.Today.Date.ToString("yyyy-MM-dd")            }
            }

            var reader = command.ExecuteReader();
            float rate = 0;
            while (reader.Read())
            {
                rate = float.Parse(reader.GetValue(0).ToString());
            }
            connection.Close();
            return Json(new { data = rate });
        }


        public async Task<ActionResult> getBudget(string ProjectCode, string BudgetLineName, bool byName)
        {
            string Budget = "";

            try
            {
                Budget = await calculateBuddget(ProjectCode, BudgetLineName, byName);
                return Json(new { data = Budget });

            }
            catch (Exception e)
            {
                return Json(new { data = 0 });

            }

        }
        private async Task<string> calculateBuddget(string ProjectCode, string BudgetLineName, bool byName)
        {
            string Budget = "";
            string BudgetLineCode = BudgetLineName;
            float AuditBudget = 0;
            try
            {

                if (byName == true)
                {


                    var getData = new getAuditData();
                    BudgetLineCode = getData.getCodeByName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), BudgetLineName, connection);
                }
                SqlCommand command = new SqlCommand("SELECT Budget FROM TblBudgetCNTran  WHERE (FirstCost=2 and SecondCost=8 and  FirstCostCode= '" + ProjectCode + "' and SecondCostCode = '" + BudgetLineCode + "');", connection);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AuditBudget = float.Parse(reader.GetValue(0).ToString());
                }
                connection.Close();


                List<ViewBudget> lst = new List<ViewBudget>();
                var c = _sc.ViewBudget.FromSql(
                  @"SELECT    SUM(dbo.TblOrderHeader.ActualTotalAmount * dbo.TblOrderHeader.OrderHeaderRate) AS Budget, dbo.TblOrderHeader.OrderHeaderProjectCode, 
                      dbo.TblOrderHeader.OrderHeaderBudgetLineCode
FROM         dbo.TblOrderHeader RIGHT OUTER JOIN
                      dbo.TblApproval ON dbo.TblOrderHeader.OrderHeaderCode = dbo.TblApproval.ApprovalHeaderCode
WHERE     (dbo.TblApproval.ApprovalIsApproved = 1)
GROUP BY dbo.TblOrderHeader.OrderHeaderProjectCode, dbo.TblOrderHeader.OrderHeaderBudgetLineCode
HAVING      (SUM(dbo.TblApproval.ApprovalIsApproved) > 1)").ToList();




                lst = c.ToList();

                lst = lst.Where(x => x.OrderHeaderProjectCode == ProjectCode && x.OrderHeaderBudgetLineCode == BudgetLineCode).ToList();

                var TotalOrders = lst.Sum(x => x.Budget);

                if (AuditBudget > 0)
                {
                    Budget = String.Format("{0:N}", AuditBudget - TotalOrders);

                }
                return Budget;

            }
            catch (Exception e)
            {
                return Budget;

            }
        }
        public ActionResult getshowhidefunder(int ordertype)
        {
            var model = new OrderViewModel();
            var res = _sc.TblOrderType.Where(o => o.orderTypeCode == ordertype).FirstOrDefault();
            if (res.orderTypeShowFunder == true)
            {
                return Json(new { data = "visible" });
            }
            else
            {
                return Json(new { data = "hidden" });
            }

        }

        public ActionResult getshowhidebudgetline(int ordertype)
        {
            var model = new OrderViewModel();
            var res = _sc.TblOrderType.Where(o => o.orderTypeCode == ordertype).FirstOrDefault();
            if (res.orderTypeShowBudgetLine == true)
            {
                return Json(new { data = "visible" });
            }
            else
            {
                return Json(new { data = "hidden" });
            }

        }

        public ActionResult getshowhideTransactionDate(int ordertype)
        {
            var model = new OrderViewModel();
            var res = _sc.TblOrderType.Where(o => o.orderTypeCode == ordertype).FirstOrDefault();
            if (res.orderTypeShowTransDate == true)
            {
                return Json(new { data = "visible" });
            }
            else
            {
                return Json(new { data = "hidden" });
            }

        }
        public async Task<ActionResult> Create()
        {

            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            var stru = _sc.TblStructure.ToList();
            var items = _sc.TblItem.ToList();
            if (stru.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Structue");
            }
            if (items.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Item");
            }
            ViewBag.Rate = 1;
            ViewBag.OrderDate = DateTime.Today;
            ViewBag.Currency = getCurrency(currency_table);
            ViewBag.ProjectName = projLoad(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync());
            ViewBag.BudgetLine = projLoad(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync());
            ViewBag.DepartmentName = _sc.TblDepartment.ToList();
            ViewBag.OrderType = _sc.TblOrderType.ToList();
            ViewBag.ItemName = _sc.TblItem.ToList();
            ViewBag.UserDepartmentName = _sc.TblDepartment.Where(x => x.departmentCode == user.userDepartmentCode).Select(u => u.departmentName).FirstOrDefault();

            return View();
        }
        //string[] headerlst,
        [HttpPost]
        public async Task<JsonResult> PostOrder(OrderHeaderClass headerlst, List<TransactionClass> transLst)
        {
            // return new JsonResult(new { success = true });
            var getData = new getAuditData();
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            try
            {
                OrderHeaderClass ohc = new OrderHeaderClass();
                ohc.OrderHeaderdepartmentCode = user.userDepartmentCode;
                ohc.OrderHeaderProjectCode = headerlst.OrderHeaderProjectCode;
                ohc.OrderHeaderdate = headerlst.OrderHeaderdate;
                ohc.OrderHeaderOrderTypeCode = headerlst.OrderHeaderOrderTypeCode;
                ohc.OrderHeaderBudgetLineCode = getData.getCodeByName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), headerlst.OrderHeaderBudgetLineCode, connection);
                ohc.OrderHeaderCurrencey = headerlst.OrderHeaderCurrencey;
                ohc.OrderHeaderRate = headerlst.OrderHeaderRate;
                // ohc.OrderHeaderRealTotal = float.Parse(headerlst[7]);
                ohc.OrderHeaderRealTotal = headerlst.OrderHeaderRealTotal;
                ohc.ActualTotalAmount = headerlst.OrderHeaderRealTotal;
                ohc.OrderHeaderNote = headerlst.OrderHeaderNote;
                ohc.OrderHeaderUserId = user.userCode;
                ohc.OrderHeaderCreationDate = DateTime.Now;
                ohc.OrderHeaderDeviceIp = 1;
                ohc.ExpectedDate = headerlst.ExpectedDate;

                _sc.Add(ohc);
                _sc.SaveChanges();
                //var tt = new TransactionClass();

                //var headId = _sc.TblOrderHeader.Max(e => e.OrderHeaderCode);// get current corder header id 
                foreach (TransactionClass tcc in transLst) // get each item as one transaction object then save it as a row database
                {
                    //tt = tcc;
                    TransactionClass tc = new TransactionClass();
                    tc.TransactionOrderHeaderCode = ohc.OrderHeaderCode;

                    var item = _sc.TblItem.Where(u => u.itemName.Equals(tcc.TransactionItemName)).Select(u => u.itemCode).FirstOrDefault();

                    tc.TransactionItemCode = item;
                    tc.TransactionQty = tcc.TransactionQty;
                    tc.TransactionDate = tcc.TransactionDate;
                    tc.TransactionPrice = tcc.TransactionPrice;
                    tc.TransactionNote = tcc.TransactionNote;
                    tc.TransactionUserId = user.userCode;
                    tc.TransactionCreationDate = DateTime.Now;
                    tc.TransactionDeviceIp = 1;
                    _sc.Add(tc);
                    _sc.SaveChanges();
                }



                //---------------------------------------------------------Save uploaded file
                // SaveUploadedFile(file, headId, user);
                int OrderHeaderCode = ohc.OrderHeaderCode;

                return new JsonResult(new { success = true, data = OrderHeaderCode });
                // return Json(new { orderHeaderCode = ohc.OrderHeaderCode });




            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                return Json(new { message = "error " + e.Message });
            }
        }

        public async Task<ActionResult> UploadFile(IFormFile file, int OrderHeaderCode)
        {
            if (file != null)
            {
                // var headId = _sc.TblOrderHeader.Max(e => e.OrderHeaderCode);// get current corder header id 
                var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userCode).FirstOrDefault();

                string uniqueFileName = null;
                if (file != null)
                {
                    string upath = Path.Combine(hostingEnviroment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(upath, uniqueFileName);

                    //    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        stream.Close();
                    }
                }

                UploadClass pc = new UploadClass();
                pc.uploadPath = uniqueFileName;
                pc.uploadHeaderCode = OrderHeaderCode;
                pc.uploadCreationDate = DateTime.Now;
                pc.uploadDeviceIp = 1;
                pc.uploadUserId = user;

                _sc.Add(pc);
                _sc.SaveChanges();
                return Json(new { message = "ok" });
            }
            return Json(new { message = "ok" });
        }

        [HttpGet]

        public IActionResult GetFiles()
        {
            List<UploadClass> lst = new List<UploadClass>();

            lst = _sc.TblUploads.Where(h => h.uploadHeaderCode == 103).ToList();
            ViewBag.lst = lst;
            return PartialView();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool IsPreview = false)
        {/*make a view model to set all properites that come from Audit DB and ours and send them as one object to the veiw*/
            var model = new OrderViewModel();
            model.headerClass = _sc.TblOrderHeader.Where(h => h.OrderHeaderCode == id).FirstOrDefault();
            model.transClass = _sc.TblTransaction.Where(t => t.TransactionOrderHeaderCode == id).ToList();
            model.projectName = projName(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync(), model.headerClass.OrderHeaderProjectCode);
            model.TotalInbasic = model.headerClass.OrderHeaderRate * model.headerClass.OrderHeaderRealTotal;
            model.budgetName = projName(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync(), model.headerClass.OrderHeaderBudgetLineCode);
            model.departmentName = _sc.TblDepartment.Where(d => d.departmentCode == model.headerClass.OrderHeaderdepartmentCode).Select(d => d.departmentName).FirstOrDefault();
            model.orderType = _sc.TblOrderType.Where(o => o.orderTypeCode == model.headerClass.OrderHeaderOrderTypeCode).Select(o => o.orderTypeName).FirstOrDefault();

            var getData = new getAuditData();
            model.headerClass.Currency = getData.getTblCodeName(currency_table, model.headerClass.OrderHeaderCurrencey.ToString(), connection);
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();

            foreach (TransactionClass tc in model.transClass)
            {
                TransactionViewModel tvm = new TransactionViewModel();
                tvm.ItemName = _sc.TblItem.Where(i => i.itemCode == tc.TransactionItemCode).Select(i => i.itemName).FirstOrDefault();
                tvm.TransactionQty = tc.TransactionQty;
                tvm.TransactionItemCode = tc.TransactionItemCode;
                tvm.TransactionPrice = tc.TransactionPrice;
                tvm.TransactionNote = tc.TransactionNote;
                tvm.TransactionDate = tc.TransactionDate;
                transactions.Add(tvm);
            }
            model.transViewModel = transactions;

            String att = _sc.TblUploads.Where(a => a.uploadHeaderCode == id).Select(a => a.uploadPath).FirstOrDefault(); // if there are more than one value make this list and loop it to fetch the wanted value
            if (att != null)// just get the readable name for the attachment
            {
                string[] attName = att.Split("_");
                model.attachName = attName[attName.Length - 1];
                ViewBag.Attach = attName[attName.Length - 1];
            }
            else
            {
                model.attachName = "";
                ViewBag.Attach = "";
            }

            ViewBag.orderView = model;
            ViewBag.Currency = getCurrency(currency_table);
            ViewBag.ProjectName = projLoad(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync());
            // ViewBag.BudgetLine = projLoad("TBLCost8");
            List<CodeNameModel> lst = new List<CodeNameModel>();
            ViewBag.BudgetLine = getData.getTableData("VRelationalBudgetLineWithFunder", "  WHERE (FirstCostCenterCode = '" + model.headerClass.OrderHeaderProjectCode + "' ) ", connection);

            ViewBag.DepartmentName = _sc.TblDepartment.ToList();
            ViewBag.OrderType = _sc.TblOrderType.ToList();
            ViewBag.ItemName = _sc.TblItem.ToList();
            ViewBag.IsPreview = IsPreview;
            ViewBag.Budget = calculateBuddget(model.headerClass.OrderHeaderProjectCode, model.headerClass.OrderHeaderBudgetLineCode, false);
            var approvals = _sc.TblApproval.Where(h => h.ApprovalIsApproved == 0 && h.ApprovalHeaderCode == id).ToList();
            if (approvals.Count > 0)
            {
                ViewBag.ShowDelete = false;
            }
            List<UploadClass> FilesLst = new List<UploadClass>();

            FilesLst = _sc.TblUploads.Where(h => h.uploadHeaderCode == id).ToList();
            ViewBag.FilesLst = FilesLst;
            var user = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.userTypeCode = user.userTypeCode;
            return View();
        }

        // get  : /orderview/EditProjectBudget

        [Authorize(Roles = "Admin, EnterProjAdmin")]
        public async Task<IActionResult> EditProjectBudget()
        {
            List<CostsViewModel> ProjectNames = projLoad(await _sc.TblGeneralPreference.Select(gp => gp.ProjectTable).FirstOrDefaultAsync());
            List<CostsViewModel> BudgetLines = projLoad(await _sc.TblGeneralPreference.Select(gp => gp.ActivitiyTable).FirstOrDefaultAsync());
            List<OrderHeaderClass> OrderHeaders = _sc.TblOrderHeader.ToList();
            ViewBag.OrderHeaders_full_list = OrderHeaders;


            //prepare data for dropdowns
            List<DropDownViewModel> DropDownViewModel_List = new List<DropDownViewModel>();
            OrderHeaders.ForEach(current =>
            {
                DropDownViewModel_List.Add(new DropDownViewModel() { Id = current.OrderHeaderCode + "", Name = current.OrderHeaderCode + "" });
            });
            ViewBag.OrderHeaders = DropDownViewModel_List;

            //for BudgetLine
            DropDownViewModel_List = new List<DropDownViewModel>();
            BudgetLines.ForEach(current =>
            {
                DropDownViewModel_List.Add(new DropDownViewModel() { Id = current.costCode, Name = current.costName });
            });
            ViewBag.BudgetLine = DropDownViewModel_List;

            //for BudgetLine
            DropDownViewModel_List = new List<DropDownViewModel>();
            ProjectNames.ForEach(current =>
            {
                DropDownViewModel_List.Add(new DropDownViewModel() { Id = current.costCode, Name = current.costName });
            });
            ViewBag.ProjectName = DropDownViewModel_List;


            //if (id != null)
            //{
            //    var order_header = await _sc.TblOrderHeader.FirstOrDefaultAsync(o => o.OrderHeaderCode == id);
            //    if (order_header == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        var project_name = "";
            //        foreach (var current in ProjectNames)
            //        {
            //            if (current.costCode == order_header.OrderHeaderProjectCode)
            //            {
            //                project_name = current.costName;
            //                break;
            //            }
            //        };
            //        var budget_line = "";
            //        foreach (var current in BudgetLines)
            //        {
            //            if (current.costCode == order_header.OrderHeaderBudgetLineCode)
            //            {
            //                budget_line = current.costName;
            //                break;
            //            }
            //        };

            //        return View(
            //            new EditProjectBudgetViewModel()
            //            {
            //                selected_budgetLines_code = order_header.OrderHeaderBudgetLineCode,
            //                selected_budgetLines_Name = budget_line,
            //                selected_ProjectName_code = order_header.OrderHeaderProjectCode,
            //                selected_ProjectName_Name = project_name,
            //                selected_OrderHeader_code = order_header.OrderHeaderCode + "",
            //                selected_OrderHeader_Name = order_header.OrderHeaderCode + ""
            //            });
            //    }

            //}

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, EnterProjAdmin")]
        public async Task<ActionResult> EditProjectBudgetPost(EditProjectBudgetViewModel editProjectBudget)
        {

            OrderHeaderClass order_to_edit = await _sc.TblOrderHeader.FirstOrDefaultAsync(o => o.OrderHeaderCode == Int32.Parse(editProjectBudget.selected_OrderHeader_code));

            if (order_to_edit == null)
            {
                return NotFound();
            }

            try
            {
                order_to_edit.OrderHeaderBudgetLineCode = editProjectBudget.selected_budgetLines_code;
                //order_to_edit.BudgetLine = editProjectBudget.selected_budgetLines_Name;
                order_to_edit.OrderHeaderProjectCode = editProjectBudget.selected_ProjectName_code;
                //order_to_edit.ProjectName = editProjectBudget.selected_ProjectName_Name;

                _sc.TblOrderHeader.Update(order_to_edit);
                await _sc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToAction(nameof(EditProjectBudget));
        }

        [HttpPost]
        public IActionResult RepushOrder(OrderHeaderClass headerlst, List<TransactionClass> transLst, int id)
        {
            {
                try
                {



                    string usr = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userCode).FirstOrDefault();
                    OrderHeaderClass t1 = new OrderHeaderClass();
                    TransactionClass t2 = new TransactionClass();
                    var res1 = _sc.TblOrderHeader.Where(i => i.OrderHeaderCode == id).FirstOrDefault();
                    // res1.OrderHeaderdepartmentCode = headerlst.OrderHeaderdepartmentCode;
                    if (headerlst.OrderHeaderProjectCode != null)
                    {
                        res1.OrderHeaderProjectCode = headerlst.OrderHeaderProjectCode;
                    }

                    res1.OrderHeaderdate = headerlst.OrderHeaderdate;
                    res1.OrderHeaderOrderTypeCode = headerlst.OrderHeaderOrderTypeCode;
                    if (headerlst.OrderHeaderBudgetLineCode != null)
                    {
                        res1.OrderHeaderBudgetLineCode = headerlst.OrderHeaderBudgetLineCode;
                    }

                    res1.OrderHeaderCurrencey = headerlst.OrderHeaderCurrencey;
                    //res1.OrderHeaderRate = Int32.Parse(headerlst[4]);
                    //res1.OrderHeaderRealTotal = Int32.Parse(headerlst[5]);
                    res1.OrderHeaderRate = headerlst.OrderHeaderRate;
                    //res1.OrderHeaderRealTotal = Int32.Parse(headerlst[7]);
                    res1.OrderHeaderRealTotal = headerlst.OrderHeaderRealTotal;
                    res1.ActualTotalAmount = headerlst.OrderHeaderRealTotal;
                    // res1.OrderHeaderCreationDate = DateTime.Now;
                    res1.OrderHeaderNote = headerlst.OrderHeaderNote;
                    // res1.OrderHeaderDeviceIp = Int32.Parse(headerlst[8]);
                    //  res1.OrderHeaderUserId = usr;
                    _sc.SaveChanges();


                    _sc.TblTransaction.RemoveRange(_sc.TblTransaction.Where(x => x.TransactionOrderHeaderCode == id));
                    _sc.SaveChanges();
                    foreach (var tc in transLst)
                    {

                        TransactionClass trans = new TransactionClass();
                        trans = tc;

                        trans.TransactionUserId = usr;
                        trans.TransactionOrderHeaderCode = id;

                        _sc.TblTransaction.Add(trans);
                        _sc.SaveChanges();

                    }

                    return Json(new { message = "ok", length = 1 });

                    //  return RedirectToAction(nameof(HomeController.Index), "Home");

                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                    return Json(new { message = "Error", length = 1 });

                }
            }
        }

        [HttpPost]
        public IActionResult SaveSupplier(int OrderCode, string SupplierCode, string supplierName)
        {
            try
            {


                OrderHeaderClass t1 = new OrderHeaderClass();

                var res1 = _sc.TblOrderHeader.Where(i => i.OrderHeaderCode == OrderCode).FirstOrDefault();

                res1.SupplierCode = SupplierCode;
                res1.SupplierName = supplierName;

                _sc.SaveChanges();
                return Json(new { message = "ok", length = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return Json(new { message = "Not saved", length = 1 });

            }
        }
        [HttpPost]
        public IActionResult SaveAcutalAmount(int OrderCode, float AcutalAmount)
        {
            try
            {
                OrderHeaderClass t1 = new OrderHeaderClass();

                var res1 = _sc.TblOrderHeader.Where(i => i.OrderHeaderCode == OrderCode).FirstOrDefault();

                res1.ActualTotalAmount = AcutalAmount;


                _sc.SaveChanges();
                return Json(new { message = "ok", length = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return Json(new { message = "Not saved", length = 1 });

            }
        }

        public ActionResult GetOrderType(int ordertype)
        {
            var model = new OrderTypeClass();
            model = _sc.TblOrderType.Where(o => o.orderTypeCode == ordertype).FirstOrDefault();
            var orderTypeNumDays = model.orderTypeNumDays;
            var orderTypeNumDaysAfter = model.orderTypeNumDaysAfter;
            return Json(new { numberOfDayesBefore = orderTypeNumDays, numberOfDayesAfter = orderTypeNumDaysAfter });


        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot" + "\\images", filename);
            //string path = Path.Combine(hostingEnviroment.WebRootPath, "images");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
            // return  RedirectToAction();
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }


        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            try
            {


                var del = _sc.TblOrderHeader.Where(i => i.OrderHeaderCode == id).FirstOrDefault();
                _sc.TblOrderHeader.Remove(del);
                _sc.SaveChanges();

                var delTrans = _sc.TblTransaction.Where(i => i.TransactionOrderHeaderCode == id).ToList();
                _sc.TblTransaction.RemoveRange(delTrans);
                _sc.SaveChanges();

                var app = _sc.TblApproval.Where(i => i.ApprovalHeaderCode == id).FirstOrDefault();
                _sc.TblApproval.Remove(app);
                _sc.SaveChanges();
                var Fileslst = _sc.TblUploads.Where(i => i.uploadHeaderCode == id).ToList();
                _sc.TblUploads.RemoveRange(Fileslst);
                _sc.SaveChanges();
                // return RedirectToAction("Index", "Home");
                return new JsonResult(new { success = true, data = "OK" });


            }
            catch (Exception e)
            {
                ViewBag.message = "ERROR " + e.Message;
                return Json(new { message = "error " + e.Message });
            }
        }


        public ActionResult DeleteFile(string filename, int id, int orderCode)
        {




            var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot" + "\\images", filename);


            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.deleteSuccess = "true";
            }

            var UploadF = new UploadClass { uploadCode = id };
            _sc.TblUploads.Attach(UploadF);
            _sc.TblUploads.Remove(UploadF);
            _sc.SaveChanges();



            return RedirectToAction("Edit", new { id = orderCode, IsPreview = true });
        }





    }
}