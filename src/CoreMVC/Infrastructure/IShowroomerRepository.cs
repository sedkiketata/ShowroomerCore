using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IShowroomerRepository
    {
        void Add(Showroomer item);
        IEnumerable<Showroomer> GetAll();
        Showroomer Find(long key);
        void Remove(long key);
        void Update(Showroomer item);
    }
}
