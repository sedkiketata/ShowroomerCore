using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Context _context;

        public CommentRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public void Add(Comment item)
        {
            _context.Comments.Add(item);
            _context.SaveChanges();
        }

        public Comment Find(long key)
        {
            return _context.Comments.FirstOrDefault(t => t.InteractionId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Comments.First(t => t.InteractionId == key);
            _context.Comments.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Comment item)
        {
            _context.Comments.Update(item);
            _context.SaveChanges();
        }
    }
}
