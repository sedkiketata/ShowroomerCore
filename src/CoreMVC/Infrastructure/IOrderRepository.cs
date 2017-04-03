using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IOrderRepository
    {
        void Add(Order item);
        IEnumerable<Order> GetAll();
        Order Find(long key);
        void Remove(long key);
        void Update(Order item);
    }
}
