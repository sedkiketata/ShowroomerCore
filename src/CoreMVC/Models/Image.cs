using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Image
    {
        ~Image() { }
        public long ImageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long ProductId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Product Product { get; set; }
    }
}
