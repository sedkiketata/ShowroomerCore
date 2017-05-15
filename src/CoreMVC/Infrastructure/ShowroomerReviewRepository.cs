using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;

namespace CoreMVC.Infrastructure
{
    public class ShowroomerReviewRepository : IShowroomerReviewRepository
    {
        private readonly Context _context;
        public ShowroomerReviewRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<ShowroomerReview> GetAllByShowroomer(long showroomerId)
        {
            List<ShowroomerReview> showroomerReviews = new List<ShowroomerReview>();
            foreach(ShowroomerReview shr in _context.ShowroomerRiviews)
            {
                if(shr.ShowroomerId == showroomerId)
                {
                    showroomerReviews.Add(shr);
                }
            }
            return showroomerReviews;
        }
        public void Add(ShowroomerReview item)
        {
           _context.Add(item);
            _context.SaveChanges();
        }

        public ShowroomerReview Find(long key)
        {
          return _context.ShowroomerRiviews.FirstOrDefault(s=>s.ShowroomerReviewId==key);
        }

        public IEnumerable<ShowroomerReview> GetAll()
        {
            return _context.ShowroomerRiviews.ToList();
        }

        public void Remove(long key)
        {
            var entity = _context.ShowroomerRiviews.FirstOrDefault(s => s.ShowroomerReviewId == key);
            _context.ShowroomerRiviews.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(ShowroomerReview item)
        {
            _context.ShowroomerRiviews.Update(item);
            _context.SaveChanges();
        }
    }
}
