﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        IQueryable<Order> GetOrders();
        Order GetOrder(Guid id);

        void DeleteOrder(Order o);

        Guid AddOrder(Order o);
    }
}
