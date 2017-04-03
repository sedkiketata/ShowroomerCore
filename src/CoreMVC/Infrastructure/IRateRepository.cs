using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IRateRepository
    {
        void Add(Rate item);
        IEnumerable<Rate> GetAll();
        Rate Find(long key);
        void Remove(long key);
        void Update(Rate item);
    }
}
