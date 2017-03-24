using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Comment : Interaction
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
