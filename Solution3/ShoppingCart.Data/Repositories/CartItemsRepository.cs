using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartItemsRepository : ICartItemsRepository
    {
        ShoppingCartDbContext _context;
        public CartItemsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }

        public CartItem GetCartItem(int id)
        {
            return _context.CartItems.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CartItem> GetCartItems()
        {
            return _context.CartItems;
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            var curr = _context.CartItems.SingleOrDefault(x => x.Id == cartItem.Id);

            if(curr != null)
            {
                curr.Qty = cartItem.Qty;

                _context.Entry(curr);
                _context.SaveChanges();
            }
        }
    }
}
