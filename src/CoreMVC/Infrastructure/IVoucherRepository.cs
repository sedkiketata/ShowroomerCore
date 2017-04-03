using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
   public interface IVoucherRepository
    {
        void Add(Voucher item);
        IEnumerable<Voucher> GetAll();
        Voucher Find(long key);
        void Remove(long key);
        void Update(Voucher item);
    }
}
