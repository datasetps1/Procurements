using Microsoft.AspNetCore.Http;
using MVCWebAppServierCon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class OrderViewModel
    {
        public OrderHeaderClass headerClass { get; set; }
        public List<TransactionClass> transClass { get; set; }

        public string departmentName { get; set; }
        public string projectName { get; set; }
        public string budgetName { get; set; }
        public string orderType { get; set; }
        public string attachName { get; set; }
        public double TotalInbasic { get; set; }
        public List<TransactionViewModel> transViewModel { get; set; }

        public IFormFile uploadPath { get; set; }

    }
}
