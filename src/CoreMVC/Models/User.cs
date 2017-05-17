using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class User
    {
        ~User() { }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public string Username { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int ZipCode { get; set; }
        public string CompanyManaged { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Interaction> Interactions { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Order> Orders { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Voucher> Vouchers { get; set; }

    }
}
