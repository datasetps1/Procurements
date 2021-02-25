using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class CurrencyViewModel
    {
        public int currencyCode { get; set; }
        public String currencyName { get; set; }
        public String currencyRate { get; set; }
    }
}
