using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface ICommentRepository
    {
        void Add(Comment item);
        IEnumerable<Comment> GetAll();
        Comment Find(long key);
        void Remove(long key);
        void Update(Comment item);
    }
}
