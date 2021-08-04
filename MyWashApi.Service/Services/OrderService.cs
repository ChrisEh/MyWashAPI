using MyWashApi.Data.Models;
using MyWashApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public List<Order> GetPendingOrders()
        {
            return _orderRepository.GetPendingOrders();
        }

        public List<Order> GetCompletedOrders()
        {
            return _orderRepository.GetCompletedOrders();
        }

        public List<Order> GetOrderDetails(Guid orderId)
        {
            return _orderRepository.GetOrderDetails(orderId);
        }

        public int GetUncompletedOrdersCount()
        {
            return _orderRepository.GetUncompletedOrdersCount();
        }

        public List<Order> GetOrdersOfUser(Guid userId)
        {
            return _orderRepository.GetOrdersOfUser(userId);
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            return await _orderRepository.CreateOrder(newOrder);
        }

        public async Task CompleteOrder(Guid orderId)
        {
            await _orderRepository.CompleteOrder(orderId);
        }
    }
}
