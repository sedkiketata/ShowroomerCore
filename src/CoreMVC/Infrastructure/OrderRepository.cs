using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        private readonly IPurchaseRepository _purchaseRespository;
        private readonly IProductRepository _productRespository;

        public OrderRepository(Context context , IPurchaseRepository purchaseRespository, IProductRepository productRespository)
        {
            _context = context;
            _purchaseRespository = purchaseRespository;
            _productRespository = productRespository;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Add(Order item)
        {
            _context.Orders.Add(item);
            _context.SaveChanges();
        }

        public Order Find(long key)
        {
            return _context.Orders.FirstOrDefault(t => t.OrderId == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Orders.First(t => t.OrderId == key);
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);
            _context.SaveChanges();
        }


        #region AddtoCart Method
        public void AddtoCart(long idUser, long idProduct, int Quantity)
        {
            long check = 0;
            IEnumerable<long> distinctedPurchaseId = (from p in GetAll()
                                                     where p.UserId == idUser
                                                     select p.PurchaseId).Distinct();
            foreach (int i in distinctedPurchaseId)
            {
                //Purchase p = UOW.GetRepository<Purchase>().GetById(i);
                Purchase p = _purchaseRespository.Find(i);
                if (p.Status.Equals("Cart"))
                {
                    check = p.PurchaseId;
                }
            }
            if (check == 0)
            { 
                Purchase pp = new Purchase
                {
                    DatePurchase = new DateTime(),
                    Total = (_productRespository.Find(idProduct).Price) * Quantity,
                    Status = "Cart"
                };
                _purchaseRespository.Add(pp);
              //  UOW.Commit();
                long idNewPurchase = _purchaseRespository.GetAll().Select(x => x.PurchaseId).Max();

                Order o = new Order
                {
                    PurchaseId = idNewPurchase,
                    ProductId = idProduct,
                    UserId = idUser,
                    Quantity = Quantity
                };
                Add(o);
               // UOW.GetRepository<Order>().Add(o);
               // UOW.Commit();
            }

            else
            {
                long checkProduct = 0;
               
                Purchase pp = _purchaseRespository.Find(check);
                pp.Total = pp.Total + (_productRespository.Find(idProduct).Price) * Quantity;
                _purchaseRespository.Update(pp);
               // UOW.Commit();
                long idNewPurchase = pp.PurchaseId;
                Order o = new Order
                {
                    PurchaseId = idNewPurchase,
                    ProductId = idProduct,
                    UserId = idUser,
                    Quantity = Quantity
                };
                foreach (Order order in GetAll().Where(x => x.PurchaseId == idNewPurchase))
                {
                    if (order.ProductId == idProduct)
                    {
                        order.Quantity = Quantity;
                        Update(order);
                        checkProduct = 1;
                       // UOW.Commit();
                    }

                }
                if (checkProduct != 1)
                {
                    Add(o);
                   // UOW.Commit();
                }

            }


        }
        #endregion

        #region CartOrders Method
        public IEnumerable<Order> CartOrders(long idUser)
        {
            IEnumerable<Order> allOrders = GetAll().Where(x => x.UserId == idUser);
            IEnumerable<Purchase> allPurchases = _purchaseRespository.GetAll().Where(x => x.Status.Equals("Cart"));

            return
                from purchase in allPurchases
                join order in allOrders on purchase.PurchaseId equals order.PurchaseId into purchases
                from prod2 in purchases
                select prod2;

        }
        #endregion

        #region ClearAll Method
        public void ClearAll(long idUser)
        {
            long idPurchased = CartOrders(idUser).First().PurchaseId;
            _purchaseRespository.Remove(_purchaseRespository.Find(idPurchased).PurchaseId);
          //  Commit();
        } 
        #endregion
    }
}
