using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void Add(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public Product Find(long key)
        {
            return _context.Products.FirstOrDefault(t => t.ProductId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Products.First(t => t.ProductId == key);
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            _context.Products.Update(item);
            _context.SaveChanges();
        }
    }
}
