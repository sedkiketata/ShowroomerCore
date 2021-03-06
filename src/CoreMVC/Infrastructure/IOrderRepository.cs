﻿using CoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Infrastructure
{
    public interface IOrderRepository
    {
        void Add(Order item);
        IEnumerable<Order> GetAll();
        Order Find(long key);
        void Remove(long key);
        void Update(Order item);
        void AddtoCart(long idUser, long idProduct, int Quantity);
        IEnumerable<Order> CartOrders(long idUser);
        void ClearAll(long idUser);
         void UpdatePurchase(long id);
    }
}
