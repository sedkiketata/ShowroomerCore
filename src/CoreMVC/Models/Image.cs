using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Image
    {
        public long ImageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
