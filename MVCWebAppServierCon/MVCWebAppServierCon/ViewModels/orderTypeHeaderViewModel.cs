using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MVCWebAppServierCon.Models;
namespace MVCWebAppServierCon.ViewModels
{
    public class orderTypeHeaderViewModel
    {

        public OrderTypeClass type { get; set; }
        public OrderHeaderClass header { get; set; }

        public List<OrderTypeClass> typeLst { get; set; }
        public List<OrderHeaderClass> headerLst { get; set; }
    }
}
