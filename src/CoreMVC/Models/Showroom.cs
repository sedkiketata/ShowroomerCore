using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Showroom
    {
        public long ShowroomerId { get; set; }
        public long ProductId { get; set; }
        public virtual Showroomer Showroomer { get; set; }
        public virtual Product Product { get; set; }
    }
}
