using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IInteractionRepository
    {
        void Add(Interaction item);
        IEnumerable<Interaction> GetAll();
        Interaction Find(long key);
        void Remove(long key);
        void Update(Interaction item);
    }
}
