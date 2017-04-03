using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IShowroomRepository
    {
        void Add(Showroom item);
        IEnumerable<Showroom> GetAll();
        Showroom Find(long ShowroomerId, long ProductId);
        void Remove(long ShowroomerId, long ProductId);
        void Update(Showroom item);
    }
}
