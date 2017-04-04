using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class ShowroomRepository : IShowroomRepository
    {
        private readonly Context _context;

        public ShowroomRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Showroom> GetAll()
        {
            return _context.Showrooms.ToList();
        }

        public void Add(Showroom item)
        {
            _context.Showrooms.Add(item);
            _context.SaveChanges();
        }
        public Showroom Find(long ShowroomId)
        {
            return _context.Showrooms.FirstOrDefault(t => t.ShowroomId == ShowroomId);
        }

        public void Remove(long ShowroomId)
        {
            var entity = _context.Showrooms.First(t => t.ShowroomId == ShowroomId);
            _context.Showrooms.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Showroom item)
        {
            _context.Showrooms.Update(item);
            _context.SaveChanges();
        }
    }
}
