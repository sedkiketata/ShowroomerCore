using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Order
    {
        ~Order() { }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        public long UserId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }

        public long ProductId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public long PurchaseId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Purchase Purchase { get; set; }
    }
}
