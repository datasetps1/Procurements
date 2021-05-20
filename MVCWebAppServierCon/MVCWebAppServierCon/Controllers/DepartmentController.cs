using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebAppServierCon.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCWebAppServierCon.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly SecondConnClass _sc;

        private readonly IConfiguration configuration;


        public DepartmentController(SecondConnClass sc, IConfiguration config)
        {
            _sc = sc;
            configuration = config;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Base64String = _sc.TblGeneralPreference.Select(g => g.Company_Logo).FirstOrDefault();
            ViewBag.CompName = _sc.TblGeneralPreference.Select(g => g.Company_Name).FirstOrDefault();
        }

        // GET: Department
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Index()
        {
            var result = _sc.TblDepartment.ToList();
            return View(result);
        }

        // GET: Department/Details/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Department/Create
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Create()
        {
            var users = _sc.TblUser.ToList();
            var departmentuser = _sc.ViwDepUsers.ToList();
            if (users.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "User");
            }
            var stru = _sc.TblStructure.ToList();
            if (stru.Count < 1)
            {
                TempData["ErrorMessage"] = "error";
                return RedirectToAction("create", "Structue");
            }
            ViewBag.Department = _sc.TblDepartment.ToList();
            ViewBag.Users = users;
            ViewBag.DepartmentUser = departmentuser;

            var structur = _sc.TblStructure.ToList();
            HttpRequest Request = HttpContext.Request;
            var requestLangauge = Request.HttpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>().RequestCulture.Culture.Name;

            structur.ForEach(current =>
            {
                var display_name = requestLangauge == "en-US" ? current.structureName2 : current.structureName;
                if (current.structureRank == 1)
                {
                    ViewBag.Procurement_Section = display_name;
                }
                else if (current.structureRank == 2)
                {
                    ViewBag.General_Manager = display_name;
                }
                else if (current.structureRank == 3)
                {
                    ViewBag.Financial_department = display_name;
                }
                else if (current.structureRank == 4)
                {
                    ViewBag.Manager = display_name;
                }
                else if (current.structureRank == 5)
                {
                    ViewBag.head = display_name;
                }
            });

            //ViewBag.ProjectName = projLoad();
            return View();
        }

        /*public String projLoad()
        {
            string conString = configuration.GetConnectionString("Myconnection");
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM TBLCost1;", connection);
            var reader =  command.ExecuteReader();
            String query = "";
            while (reader.Read())
            {
                query+=(reader.GetValue(0).ToString())+ "  ---- ";
                // do something with 'value'
            }
            return query;
        }*/

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Create(DepartmentClass dc)
        {
            try
            {
                dc.departmentUserId = _sc.TblUser.Where(u => u.userName.Equals(User.Identity.Name)).Select(u => u.userCode).FirstOrDefault();
                dc.departmentCreationDate = DateTime.Now;
                _sc.Add(dc);
                _sc.SaveChanges();
                ViewBag.message = "The Record " + dc.departmentName + " Is Saved Successfully... !";
                ViewBag.Department = _sc.TblDepartment.ToList();
                ViewBag.Users = _sc.TblUser.ToList();
                ViewBag.DepartmentUser = _sc.ViwDepUsers.ToList();
                return View();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Edit/5
        [Authorize(Roles = "Admin, EnterSet")]
        public ActionResult Edit(int id)
        {
            var users = _sc.TblUser.ToList();
            var res = _sc.TblDepartment.Where(d => d.departmentCode == id).FirstOrDefault();
            ViewBag.Department = _sc.TblDepartment.ToList();
            ViewBag.Users = users;
            return View(res);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SaveSet")]
        public ActionResult Edit(int id, DepartmentClass dc)
        {
            try
            {
                // TODO: Add update logic here
                var x = _sc.TblDepartment.Where(d => d.departmentCode == id).FirstOrDefault();
                x.departmentName = dc.departmentName;
                x.departmentGeneralManagerCode = dc.departmentGeneralManagerCode;
                x.departmentManagerCode = dc.departmentManagerCode;
                x.departmentHeadCode = dc.departmentHeadCode;
                x.departmentFinancialCode = dc.departmentFinancialCode;
                x.departmentProcurementSectionCode = dc.departmentProcurementSectionCode;
                x.departmentNote = dc.departmentNote;

                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, DeleteSet")]
        public ActionResult Delete(int id, DepartmentClass collection)
        {
            try
            {

                var CheckOrd = _sc.TblOrderHeader.Where(m => m.OrderHeaderdepartmentCode == id).FirstOrDefault();
                if (CheckOrd != null)
                {
                    ViewBag.message = "The department is used";
                    return RedirectToAction(nameof(Create));
                }
                var del = _sc.TblDepartment.Where(i => i.departmentCode == id).FirstOrDefault();
                _sc.TblDepartment.Remove(del);
                _sc.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }
    }
}