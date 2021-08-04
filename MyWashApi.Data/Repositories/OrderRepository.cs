using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWashApi.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _ctx.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }
        public List<Order> GetPendingOrders()
        {
            return _ctx.Orders.Where(o => !o.IsOrderCompleted).ToList();
        }

        public List<Order> GetCompletedOrders()
        {
            return _ctx.Orders.Where(o => o.IsOrderCompleted).ToList();
        }

        public List<Order> GetOrderDetails(Guid orderId)
        {
            return _ctx.Orders.Where(o => o.Id == orderId)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product).ToList();
        }

        public int GetUncompletedOrdersCount()
        {
            return _ctx.Orders.Where(o => !o.IsOrderCompleted).Count();
        }

        public List<Order> GetOrdersOfUser(Guid userId)
        {
            return _ctx.Orders.Where(o => o.User.Id == userId)
                .OrderByDescending(o => o.OrderPlaced).ToList();
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            newOrder.IsOrderCompleted = false;
            newOrder.OrderPlaced = DateTime.Now;
            _ctx.Orders.Add(newOrder);

            var shoppingCart = await _ctx.ShoppingCarts.FirstOrDefaultAsync(s => s.UserId == newOrder.User.Id);

            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                _ctx.OrderDetails.Add(new OrderDetail()
                {
                    Price = shoppingCartItem.Product.Price,
                    TotalAmount = shoppingCartItem.Product.Price * shoppingCartItem.Quantity,
                    Qty = shoppingCartItem.Quantity,
                    ProductId = shoppingCartItem.Product.Id,
                    OrderId = shoppingCartItem.Id
                });
            }

            _ctx.ShoppingCarts.Remove(shoppingCart);
            await _ctx.SaveChangesAsync();
            return newOrder;
        }

        public async Task CompleteOrder(Guid orderId)
        {
            var order = await _ctx.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.IsOrderCompleted = true;
            await _ctx.SaveChangesAsync();
        }
    }
}