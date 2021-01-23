using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService: ICartsService
    {
        private IMapper _mapper;
        private ICartsRepository _cartRepo;
        public CartsService(ICartsRepository cartsRepository
           , IMapper mapper
            )
        {
            _mapper = mapper;
            _cartRepo = cartsRepository;
        }

        public int AddCart(CartViewModel c)
        {
            Cart cart = new Cart()
            {
                Id = c.Id,
                Email = c.Email,
                price = c.Price
            };

            _cartRepo.AddCart(cart);
            return cart.Id;
        }

        public void DeleteCart(CartViewModel c)
        {
            Cart cart = _cartRepo.GetCart(c.Email);
            if(cart != null)
            {
                _cartRepo.DeleteCart(cart);
            }
        }

        public CartViewModel GetCart(string email)
        {
            Cart cart = _cartRepo.GetCart(email);
            var res = _mapper.Map<Cart, CartViewModel>(cart);
            return res;
        }

        public void UpdateCart(CartViewModel c)
        {
            Cart cart = _cartRepo.GetCart(c.Email);
            double amount = 0;
            foreach(var crt in cart.CartItems)
            {
                amount = amount + crt.Product.Price + crt.Qty;
            }
            cart.price = amount;
            _cartRepo.UpdateCart(cart);

            var res = _mapper.Map<Cart, CartViewModel>(cart);
        }

    }
}
