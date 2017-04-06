using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Showroomer : User
    {
        ~Showroomer() { }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Showroom> Showrooms { get; set; }
    }
}
