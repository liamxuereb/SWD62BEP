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
    public class OrdersService : IOrdersService
    {
        private IMapper _mapper;
        private IOrdersRepository _ordersRepo;
        public OrdersService(IOrdersRepository ordersRepository
           , IMapper mapper
            )
        {
            _mapper = mapper;
            _ordersRepo = ordersRepository;
        }

        public void AddOrder(OrderViewModel order)
        {

            var myOrder = _mapper.Map<Order>(order);

            _ordersRepo.AddOrder(myOrder);

        }
        public void DeleteOrder(Guid id)
        {
            var oToDelete = _ordersRepo.GetOrder(id);

            if (oToDelete != null)
            {
                _ordersRepo.DeleteOrder(oToDelete);
            }
        }

        public OrderViewModel GetOrder(Guid id)
        {

            var myOrder = _ordersRepo.GetOrder(id);
            var result = _mapper.Map<OrderViewModel>(myOrder);
            return result;

        }

        public IQueryable<OrderViewModel> GetOrders()
        {

            var orders = _ordersRepo.GetOrders().ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);

            return orders;

        }
    }
}
