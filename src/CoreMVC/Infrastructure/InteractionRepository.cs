using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class InteractionRepository : IInteractionRepository
    {
        private readonly Context _context;

        public InteractionRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Interaction> GetAll()
        {
            return _context.Interactions.ToList();
        }

        public void Add(Interaction item)
        {
            _context.Interactions.Add(item);
            _context.SaveChanges();
        }

        public Interaction Find(long key)
        {
            return _context.Interactions.FirstOrDefault(t => t.InteractionId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Interactions.First(t => t.InteractionId == key);
            _context.Interactions.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Interaction item)
        {
            _context.Interactions.Update(item);
            _context.SaveChanges();
        }
    }
}
