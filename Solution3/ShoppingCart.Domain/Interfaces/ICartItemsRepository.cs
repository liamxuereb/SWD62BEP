using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartItemsRepository
    {
        CartItem GetCartItem(int id);

        IQueryable<CartItem> GetCartItems();

        void AddCartItem(CartItem cartItem);

        void DeleteCartItem(CartItem cartItem);

        void UpdateCartItem(CartItem cartItem);
    }
}
