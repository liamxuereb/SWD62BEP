using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        CartViewModel GetCart(string email);

        int AddCart(CartViewModel cart);

        void DeleteCart(CartViewModel cart);

        void UpdateCart(CartViewModel cart);

    }
}
