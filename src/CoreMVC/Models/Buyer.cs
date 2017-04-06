using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Buyer : User
    {
        ~Buyer() { }
        public string DeliveryAddress { get; set; }
    }
}
