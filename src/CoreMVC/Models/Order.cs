using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public long PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
}
