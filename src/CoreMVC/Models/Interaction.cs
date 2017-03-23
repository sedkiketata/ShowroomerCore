using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Interaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InteractionId { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
