using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;

namespace CoreMVC.Infrastructure
{
    public class PurchasesRepository : IPurchaseRepository
    {
        private readonly Context _context;

        public PurchasesRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases.ToList();
        }

        public void Add(Purchase item)
        {
            _context.Purchases.Add(item);
            _context.SaveChanges();
        }

        public Purchase Find(long key)
        {
            return _context.Purchases.FirstOrDefault(t => t.PurchaseId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Purchases.First(t => t.PurchaseId == key);
            _context.Purchases.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Purchase item)
        {
            _context.Purchases.Update(item);
            _context.SaveChanges();
        }
    }
}
