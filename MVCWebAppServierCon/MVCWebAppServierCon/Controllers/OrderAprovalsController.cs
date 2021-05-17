using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppServierCon.Models;

namespace MVCWebAppServierCon.Controllers
{
    public class OrderAprovalsController : Controller
    {
        private readonly SecondConnClass _context;

        public OrderAprovalsController(SecondConnClass context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DisplayApprovals(int? orderCode)
        {
            if (orderCode == null)
            {
                return View(new List<ApprovalViewModel>());
            }
            List<ApprovalViewModel> results = (from a in _context.TblApproval
                                               join u in _context.TblUser
                                               on a.ApprovalUserId equals u.userCode
                                               where a.ApprovalHeaderCode == orderCode
                                               select new ApprovalViewModel()
                                               {
                                                   Id = a.ApprovalCode,
                                                   ApprovalHeaderCode = a.ApprovalHeaderCode,
                                                   ApprovalIsApproved = a.ApprovalIsApproved,
                                                   ApprovalNote = a.ApprovalNote,
                                                   ApprovalUserName = u.userName,
                                                   ApprovalCreationDate = a.ApprovalCreationDate,
                                                   ApprovalDeviceIp = a.ApprovalDeviceIp,
                                                   ToUser = a.ToUser,

                                               }).ToList();

            List<ApprovalViewModel> results2 = (from result in results
                                                join u in _context.TblUser
                                                on result.ToUser equals u.userCode
                                                select new ApprovalViewModel()
                                                {
                                                    Id = result.Id,
                                                    ApprovalHeaderCode = result.ApprovalHeaderCode,
                                                    ApprovalIsApproved = result.ApprovalIsApproved,
                                                    ApprovalStatus = GetStatusName(result.ApprovalIsApproved),
                                                    ApprovalNote = result.ApprovalNote,
                                                    ApprovalUserName = result.ApprovalUserName,
                                                    ApprovalCreationDate = result.ApprovalCreationDate,
                                                    ApprovalDeviceIp = result.ApprovalDeviceIp,
                                                    ToUserName = u.userName,
                                                    ToUser = result.ToUser,

                                                }).ToList();


            return View(results2);
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approval = await _context.TblApproval.FirstOrDefaultAsync(a => a.ApprovalCode == id);

            if (approval == null)
            {
                return NotFound();
            }

            return View(approval);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? orderCode)
        {
            var approval = await _context.TblApproval.FindAsync(id);
            _context.TblApproval.Remove(approval);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DisplayApprovals), new { orderCode = orderCode });
        }
    }
    public class ApprovalViewModel
    {

        public int Id { get; set; }

        public int ApprovalHeaderCode { get; set; }

        public int ApprovalIsApproved { get; set; }
        public string ApprovalStatus { get; set; }

        public String ApprovalNote { get; set; }

        public String ApprovalUserName { get; set; }

        public DateTime ApprovalCreationDate { get; set; }

        public int ApprovalDeviceIp { get; set; }

        public String ToUser { get; set; }
        public String ToUserName { get; set; }

    }
}