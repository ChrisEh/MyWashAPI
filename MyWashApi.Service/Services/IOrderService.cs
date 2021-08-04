using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Service.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(Guid orderId);
        List<Order> GetPendingOrders();
        List<Order> GetCompletedOrders();
        List<Order> GetOrderDetails(Guid orderId);
        int GetUncompletedOrdersCount();
        List<Order> GetOrdersOfUser(Guid userId);
        Task<Order> CreateOrder(Order newOrder);
        Task CompleteOrder(Guid orderId);
    }
}