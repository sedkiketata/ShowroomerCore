using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IProductRepository
    {
        void Add(Product item);
        IEnumerable<Product> GetAll();
        Product Find(long key);
        void Remove(long key);
        void Update(Product item);
    }
}
