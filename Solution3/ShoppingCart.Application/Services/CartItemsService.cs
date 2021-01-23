using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartItemsService: ICartItemsService
    {
        private IMapper _mapper;
        private ICartItemsRepository _cartItemsRepo;
        private ICartsRepository _cartsRepo;
        public CartItemsService(ICartItemsRepository cartItemsRepository, ICartsRepository cartsRepository
           , IMapper mapper
            )
        {
            _mapper = mapper;
            _cartItemsRepo = cartItemsRepository;
            _cartsRepo = cartsRepository;
        }

        public void AddCartItem(CartItemViewModel c)
        {
            var item = _mapper.Map<CartItem>(c);
            _cartItemsRepo.AddCartItem(item);
        }

        public void DeleteCartItem(CartItemViewModel c)
        {
            var item = _cartItemsRepo.GetCartItem(c.Id);

            if(item != null)
            {
                _cartItemsRepo.DeleteCartItem(item);
            }
        }

        public CartViewModel GetCart(string email)
        {
            var cart = _cartsRepo.GetCart(email);
            var result = _mapper.Map<Cart, CartViewModel>(cart);
            return result;
        }

        public CartItemViewModel GetCartItem(int c)
        {
            var cart = _cartItemsRepo.GetCartItem(c);
            var result = _mapper.Map<CartItemViewModel>(cart);
            return result;
        }

        public void UpdateCartItem(CartItemViewModel c)
        {
            var item = _cartItemsRepo.GetCartItem(c.Id);
            if(item != null)
            {
                item.Qty = item.Qty + 1;
                _cartItemsRepo.UpdateCartItem(item);
            }
        }

        public IQueryable<CartItemViewModel> GetCartItems()
        {
            var items = _cartItemsRepo.GetCartItems().ProjectTo<CartItemViewModel>(_mapper.ConfigurationProvider);

            return items;
        }

        public CartItemViewModel GetCartProduct(int cart, Guid id)
        {
            var item = _cartItemsRepo.GetCartItems().Where(c => c.CartID == cart && c.Product.Id == id).FirstOrDefault();

            var res = _mapper.Map<CartItem, CartItemViewModel>(item);
            return res;
        }

        
    }
}
