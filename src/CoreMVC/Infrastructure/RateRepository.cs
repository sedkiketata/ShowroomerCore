using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class RateRepository : IRateRepository
    {
        private readonly Context _context;

        public RateRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Rate> GetAll()
        {
            return _context.Rates.ToList();
        }

        public void Add(Rate item)
        {
            _context.Rates.Add(item);
            _context.SaveChanges();
        }

        public Rate Find(long key)
        {
            return _context.Rates.FirstOrDefault(t => t.InteractionId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Rates.First(t => t.InteractionId == key);
            _context.Rates.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Rate item)
        {
            _context.Rates.Update(item);
            _context.SaveChanges();
        }
    }
}
