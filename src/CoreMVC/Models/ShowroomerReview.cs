using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class ShowroomerReview
    {
        ~ShowroomerReview() { }
        public int ShowroomerReviewId { get; set; }
        public int ShowroomerId { get; set; }
        public int UserId { get; set; }
        public int Mark { get; set; }
        public String Comment { get; set; }
        public DateTime date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Showroomer Showroomer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual User User { get; set; }
    }
}
