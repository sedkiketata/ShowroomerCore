using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Purchase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseId { get; set; }
        public DateTime DatePurchase { get; set; }
        public Double Total { get; set; }
        public String Status { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
