using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class CompleteActivity
    {
        [Key]
        public int Id { get; set; }
        //TBLorderheader 
        public int SupplierId { get; set; }
        public string Description { get; set; }

        [Display(Name = "Order Code")]
        public int OrderCode { get; set; }

        [Display(Name = "Book Number")]
        public string BookNumber { get; set; }

        [Display(Name = "Activity Venue")]
        public string ActivityVenue { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "Done Tasks")]
        public string DoneTasks { get; set; }

        [Display(Name = "Coordinator Name")]
        public string CoordinatorName { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }

        public ICollection<CompleteActivityFiles> activityFiles { get; set; }
        public ICollection<CompleteActivityOffered> activityOffereds { get; set; }

        public static implicit operator CompleteActivity(StringValues v)
        {
            throw new NotImplementedException();
        }
    }
}
