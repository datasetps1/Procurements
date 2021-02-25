using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionCode { get; set; }
        public int TransactionOrderHeaderCode { get; set; }
        public int TransactionItemCode { get; set; }
        public double TransactionQty { get; set; }
        public float TransactionPrice { get; set; }

        public String ItemName { get; set; }

        public String TransactionNote { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
