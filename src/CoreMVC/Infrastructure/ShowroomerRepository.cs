using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class ShowroomerRepository : IShowroomerRepository
    {
        private readonly Context _context;

        public ShowroomerRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Showroomer> GetAll()
        {
            return _context.Showroomers.ToList();
        }

        public void Add(Showroomer item)
        {
            _context.Showroomers.Add(item);
            _context.SaveChanges();
        }

        public Showroomer Find(long key)
        {
            return _context.Showroomers.FirstOrDefault(t => t.UserId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Showroomers.First(t => t.UserId == key);
            _context.Showroomers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Showroomer item)
        {
            _context.Showroomers.Update(item);
            _context.SaveChanges();
        }
    }
}
