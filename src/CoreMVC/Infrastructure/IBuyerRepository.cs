using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IBuyerRepository
    {
        void Add(Buyer item);
        IEnumerable<Buyer> GetAll();
        Buyer Find(long key);
        void Remove(long key);
        void Update(Buyer item);
    }
}
