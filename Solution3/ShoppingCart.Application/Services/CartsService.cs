using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
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

        public CartViewModel GetCart()
        {
            var myCart = _cartRepo.GetCart();
            var result = _mapper.Map<CartViewModel>(myCart);
            return result;
        }

    }
}
