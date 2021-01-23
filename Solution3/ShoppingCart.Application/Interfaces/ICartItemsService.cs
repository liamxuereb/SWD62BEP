using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartItemsService
    {
        IQueryable<CartItemViewModel> GetCartItems();

        CartItemViewModel GetCartItem(int id);

        CartViewModel GetCart(string email);

        CartItemViewModel GetCartProduct(int cart, Guid id);

        void AddCartItem(CartItemViewModel cartItem);

        void DeleteCartItem(CartItemViewModel cartItem);

        void UpdateCartItem(CartItemViewModel cartItem);
    }
}
