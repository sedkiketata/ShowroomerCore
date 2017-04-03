using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Add(Order item)
        {
            _context.Orders.Add(item);
            _context.SaveChanges();
        }

        public Order Find(long key)
        {
            return _context.Orders.FirstOrDefault(t => t.OrderId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Orders.First(t => t.OrderId == key);
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);
            _context.SaveChanges();
        }
    }
}
