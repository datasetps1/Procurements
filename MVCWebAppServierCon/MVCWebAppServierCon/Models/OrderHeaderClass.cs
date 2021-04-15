using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCWebAppServierCon.ViewModels;

namespace MVCWebAppServierCon.Models
{
    public class OrderHeaderClass
    {
        [Key]
        public int OrderHeaderCode { get; set; }
        [Required]
        public int OrderHeaderdepartmentCode { get; set; }
        [Required]
        public string OrderHeaderProjectCode { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //   [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderHeaderdate { get; set; }
        [Required]
        public int OrderHeaderOrderTypeCode { get; set; }
        [Required]
        public string OrderHeaderBudgetLineCode { get; set; }// بند النشاط
        [Required]
        public String OrderHeaderCurrencey { get; set; }
        [Required]
        public float OrderHeaderRate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public float OrderHeaderRealTotal { get; set; }
        public String OrderHeaderNote { get; set; }
      
        [Required]
        [StringLength(450)]
        public string OrderHeaderUserId { get; set; }
        [Required]
        public DateTime OrderHeaderCreationDate { get; set; }
        [Required]
        public int OrderHeaderDeviceIp { get; set; }
    
        [NotMapped]
        public string ProjectName { get; set; }
        [NotMapped]
        public string BudgetLine { get; set; }
        [NotMapped]
        public string Currency { get; set; }
        [NotMapped]
        public string OrderTypeName { get; set; }
        [NotMapped]
        public string UserName { get; set; }

        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }

        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public int StatusCode { get; set; }
        [NotMapped]
        public string LastUserName { get; set; }
        [NotMapped]
        public string waitingUser { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpectedDate { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double TotalInbasic { get; set; }

        [NotMapped]
        public string NotesFromLastAction { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public float ActualTotalAmount { get; set; }
        [NotMapped]
        public Boolean NeedDeductionAtsource { get; set; }
        [NotMapped]
        public int PriceQuoteAmount { get; set; }
        [NotMapped]
        public Boolean IsNeedPriceQuote { get; set; }
        public Boolean Freaze { get; set; }
        public string FreazeNote { get; set; }

        public string Cost3 { get; set; }

        public string Cost4 { get; set; }

        public string Doner { get; set; }

    }
}
