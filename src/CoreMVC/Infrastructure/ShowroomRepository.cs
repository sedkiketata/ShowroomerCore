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
        /// <summary>
        /// This Method will return a Showroom object by entering ShowroomerId and ProductId
        /// </summary>
        /// <param name="ShowroomerId">Put the right id of the Showroomer</param>
        /// <param name="ProductId">Put the right id of the Product</param>
        /// <returns></returns>
        public Showroom Find(long ShowroomerId, long ProductId)
        {
            return _context.Showrooms.FirstOrDefault(t => t.ShowroomerId == ShowroomerId && t.ProductId == ProductId );
        }

        /// <summary>
        /// This Method will delete a Showroom object specified by a ShowroomerId and ProductId
        /// </summary>
        /// <param name="ShowroomerId">Put the right id of the Showroomer</param>
        /// <param name="ProductId">Put the right id of the Product</param>
        public void Remove(long ShowroomerId, long ProductId)
        {
            var entity = _context.Showrooms.First(t => t.ShowroomerId == ShowroomerId && t.ProductId == ProductId);
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
