using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
   public interface IShowroomerReviewRepository
    {
        void Add(ShowroomerReview item);
        IEnumerable<ShowroomerReview> GetAll();
        IEnumerable<ShowroomerReview> GetAllByShowroomer(long Id);
        ShowroomerReview Find(long key);
        void Remove(long key);
        void Update(ShowroomerReview item);
    }
}
