using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly Context _context;

        public BuyerRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Buyer> GetAll()
        {
            return _context.Buyers.ToList();
        }

        public void Add(Buyer item)
        {
            _context.Buyers.Add(item);
            _context.SaveChanges();
        }

        public Buyer Find(long key)
        {
            return _context.Buyers.FirstOrDefault(t => t.UserId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Buyers.First(t => t.UserId == key);
            _context.Buyers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Buyer item)
        {
            _context.Buyers.Update(item);
            _context.SaveChanges();
        }
    }
}
