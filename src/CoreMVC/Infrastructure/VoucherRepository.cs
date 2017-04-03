using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly Context _context;
        public VoucherRepository(Context context)
        {
            _context = context;


        }
        public IEnumerable<Voucher> GetAll()
        {
            return _context.Vouchers.ToList();
        }

        public void Add(Voucher item)
        {
            _context.Vouchers.Add(item);
            _context.SaveChanges();
        }

        public Voucher Find(long key)
        {
            return _context.Vouchers.FirstOrDefault(t => t.VoucherId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Vouchers.First(t => t.VoucherId == key);
            _context.Vouchers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Voucher item)
        {
            _context.Vouchers.Update(item);
            _context.SaveChanges();
        }
    }
}
