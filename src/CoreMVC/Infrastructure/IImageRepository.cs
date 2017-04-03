using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IImageRepository
    {
        void Add(Image item);
        IEnumerable<Image> GetAll();
        Image Find(long key);
        void Remove(long key);
        void Update(Image item);
    }
}
