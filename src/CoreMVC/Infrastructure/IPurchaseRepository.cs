using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IPurchaseRepository
    {

        void Add(Purchase item);
        IEnumerable<Purchase> GetAll();
        Purchase Find(long key);
        void Remove(long key);
        void Update(Purchase item);
    }
}
