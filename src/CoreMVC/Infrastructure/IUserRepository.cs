using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IUserRepository
    {
        void Add(User item);
        IEnumerable<User> GetAll();
        User Find(long key);
        void Remove(long key);
        void Update(User item);
    }
}
