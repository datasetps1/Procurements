using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class CompleteActivityOffered
    {
        [Key]
        public int Id { get; set; }

        public string Offered_to_us { get; set; }

        [ForeignKey("CompleteActivity")]
        public int CompleteActivityId { get; set; }
        public CompleteActivity CompleteActivity { get; set; }
    }
}
