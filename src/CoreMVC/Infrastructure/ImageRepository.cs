using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class ImageRepository : IImageRepository
    {
        private readonly Context _context;

        public ImageRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Images.ToList();
        }

        public void Add(Image item)
        {
            _context.Images.Add(item);
            _context.SaveChanges();
        }

        public Image Find(long key)
        {
            return _context.Images.FirstOrDefault(t => t.ImageId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Images.First(t => t.ImageId == key);
            _context.Images.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Image item)
        {
            _context.Images.Update(item);
            _context.SaveChanges();
        }
    }
}
