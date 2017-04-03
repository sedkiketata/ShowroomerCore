using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Buyer : User
    {
        public string DeliveryAddress { get; set; }
    }
}
